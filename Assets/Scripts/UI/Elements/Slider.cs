using System;
using Managers;
using Runtime;
using UI.Layout;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Slider")]
	public sealed class Slider : ElementBase, IKnobReceiver, ILayoutCallback, ILayoutInputCallback, IPrewarm
	{
		[SerializeField] private Image slider;
		[SerializeField] private Knob knob;
		[SerializeField] private Axis direction;
		[SerializeField] private ElementColor color;
		[SerializeField] private bool wholeNumbers;
		[SerializeField] [HideInInspector] private float lowerValue;
		[SerializeField] [HideInInspector] private float upperValue = 1f;
		[SerializeField] [HideInInspector] private int lowerValueInt;
		[SerializeField] [HideInInspector] private int upperValueInt = 1;
		[SerializeField] [HideInInspector] private float speedMultiplier = 1f;

		private ISliderReceiver m_receiver;
		private float m_position;
		private float m_value;
		private float m_intTimer;
		private int m_intValue;
		private bool m_selected;
		
		public bool WholeNumbers => wholeNumbers;
		public float LowerValue => lowerValue;
		public float UpperValue => upperValue;
		public int LowerValueInt => lowerValueInt;
		public int UpperValueInt => upperValueInt;
		
		private RectTransform SliderArea => slider.rectTransform;

		public void SetReceiver(ISliderReceiver receiver) => m_receiver = receiver;
		
		public void OnSelected()
		{
			knob.OnSelected();
			color.ApplySelected(slider);
			m_selected = true;
		}

		public void OnDeselected()
		{
			knob.OnDeselected();
			color.ApplyEnabled(slider);
			m_selected = false;
		}

		public void OnInput(Vector2 axis, ref Direction input)
		{
			var wholeInput = false;
			bool isValueAtExtremes;
			if (wholeNumbers) isValueAtExtremes = m_intValue == lowerValueInt || m_intValue == upperValueInt;
			else isValueAtExtremes = m_value is 0f or 1f;
			
			var bounds = SliderArea.rect.size;
			float inputAxis;
			float upperBounds;
			switch (direction)
			{
				case Axis.Horizontal:
					upperBounds = bounds.x;
					inputAxis = axis.x;
					break;
				case Axis.Vertical:
					upperBounds = bounds.y;
					inputAxis = axis.y;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (input.ToAxis() == direction && !isValueAtExtremes)
			{
				input = UIConstants.Direction_None;
				wholeInput = true;
			}

			if (inputAxis == 0f) return;
			if (wholeNumbers)
			{
				if (wholeInput)
				{
					m_intTimer = 0f;
					UpdateInteger(Math.Sign(inputAxis), upperBounds, true);
				}
				else
				{
					m_intTimer += Time.deltaTime * Math.Abs(inputAxis);
					if (m_intTimer > 1f)
					{
						m_intTimer -= 1f;
						UpdateInteger(Math.Sign(inputAxis), upperBounds, true);
					}
				}
			}
			else UpdateFloat(inputAxis * Time.deltaTime * speedMultiplier, upperBounds, true);
		}

		private float GetUpperBounds()
		{
			var bounds = SliderArea.rect.size;
			float upperBounds;
			switch (direction)
			{
				case Axis.Horizontal:
					upperBounds = bounds.x;
					break;
				case Axis.Vertical:
					upperBounds = bounds.y;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return upperBounds;
		}
		
		public void SetInteger(int value, bool callback) => SetIntegerInternal(value, GetUpperBounds(), callback);

		private void SetIntegerInternal(int value, float upperBounds, bool callback)
		{
			value = Mathf.Clamp(value, lowerValueInt, upperValueInt);
			if (m_intValue == value) return;
			m_intValue = value;
			m_value = CalculateFloatFromInteger();
			UpdateFromInteger(CalculateIntegerPosition(upperBounds), true, callback);
		}
		
		private void UpdateInteger(int change, float upperBounds, bool callback)
		{
			var newIntValue = Mathf.Clamp(m_intValue + change, lowerValueInt, upperValueInt);
			if (m_intValue == newIntValue) return;
			m_intValue = newIntValue;
			m_value = CalculateFloatFromInteger();
			UpdateFromInteger(CalculateIntegerPosition(upperBounds), true, callback);
		}

		public void SetFloat(float value, bool callback) => SetFloatInternal(value, GetUpperBounds(), callback);

		private void SetFloatInternal(float value, float upperBounds, bool callback)
		{
			value = Mathf.Clamp01(value);
			if (m_value == value) return;
			m_value = value;
			m_intValue = CalculateIntegerFromFloat();
			m_position = CalculateFloatPosition(upperBounds);
			UpdateFromFloating(m_position, true, callback);
		}
		
		private void UpdateFloat(float change, float upperBounds, bool callback)
		{
			var newValue = Mathf.Clamp01(m_value + change);
			if (m_value == newValue) return;
			m_value = newValue;
			m_intValue = CalculateIntegerFromFloat();
			m_position = CalculateFloatPosition(upperBounds);
			UpdateFromFloating(m_position, true, callback);
		}
		
		public void OnKnobHover(bool isMouseOver)
		{
			if (isMouseOver)
			{
				if (LayoutParent) LayoutManager.Instance.Select(LayoutParent);
				else OnSelected();
			}
			else if (!LayoutParent) OnDeselected();
		}
		
		public void OnKnobMovement(Vector2 moveDelta)
		{
			var bounds = SliderArea.rect.size;
			float upperBounds;
			float newPosition;
			switch (direction)
			{
				case Axis.Horizontal:
					upperBounds = bounds.x;
					newPosition = m_position + moveDelta.x;
					break;
				case Axis.Vertical:
					upperBounds = bounds.y;
					newPosition = m_position + moveDelta.y;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var newValue = Test(ref newPosition, upperBounds);
			var updatePosition = newPosition != m_position;
			if (updatePosition) m_position = newPosition;
			if (m_value == newValue) return;
			m_value = newValue;
			if (wholeNumbers)
			{
				var newIntValue = CalculateIntegerFromFloat();
				if (m_intValue != newIntValue)
				{
					m_intValue = newIntValue;
					UpdateFromInteger(CalculateIntegerPosition(upperBounds), updatePosition, true);
				}
			}
			else UpdateFromFloating(newPosition, updatePosition, true);
		}

		private int CalculateIntegerFromFloat() => lowerValueInt + (int) ((upperValueInt - lowerValueInt) * m_value);
		
		private float CalculateIntegerPosition(float upperBounds) => 
			((m_intValue - lowerValueInt) / (float) (upperValueInt - lowerValueInt)) * upperBounds;

		private void UpdateFromInteger(float newPosition, bool updatePosition, bool callback)
		{
			if (updatePosition) SetPosition(newPosition);
			if (callback) m_receiver?.OnSliderChanged(m_intValue);
		}

		private float CalculateFloatFromInteger() => 
			(m_intValue - lowerValueInt) - (float) (upperValueInt - lowerValueInt);

		private float CalculateFloatPosition(float upperBounds) => m_value * upperBounds;
		
		private void UpdateFromFloating(float newPosition, bool updatePosition, bool callback)
		{
			if (updatePosition) SetPosition(newPosition);
			if (callback) m_receiver?.OnSliderChanged(Mathf.Lerp(lowerValue, upperValue, m_value));
		}
		
		private void SetPosition(float position)
		{
			switch (direction)
			{
				case Axis.Horizontal:
					knob.RectTransform.anchoredPosition = new Vector2(position, 0f);
					break;
				case Axis.Vertical:
					knob.RectTransform.anchoredPosition = new Vector2(0f, position);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private static float Test(ref float value, float upper)
		{
			if (value < 0f) value = 0f;
			else if (value > upper) value = upper;

			return value / upper;
		}

		private void OnEnable()
		{
			if (m_selected)
			{
				color.ApplySelected(slider);
				knob.OnSelected();
			}
			else
			{
				color.ApplyEnabled(slider);
				knob.OnDeselected();
			}
			
			knob.enabled = true;
		}

		private void OnDisable()
		{
			color.ApplyDisabled(slider);
			knob.enabled = false;
		}

		public void OnPrewarm()
		{
			knob.SetReceiver(this);
			var layoutParent = LayoutParent;
			if (layoutParent)
			{
				layoutParent.SetCallback(this);
				layoutParent.SetControlCallback(this);
			}
		}
		
		private void OnDestroy() => m_receiver = null;
		
		private void OnValidate()
		{
			color.Validate(slider, enabled);
			knob.enabled = enabled;
		}
	}
}