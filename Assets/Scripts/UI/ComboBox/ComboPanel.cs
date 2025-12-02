using System;
using System.Collections.Generic;
using Attributes;
using Callbacks.ComboBox;
using Callbacks.Layout;
using Callbacks.Prewarm;
using Managers;
using Runtime.Automation;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/ComboBox Dropdown Panel")]
	public sealed class ComboPanel : ListBase, IHierarchyChanged, ILayoutCallback, IPrewarm
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(RectTransform))]
		private RectTransform rectTransform;
		[SerializeField] private Image background;
		[SerializeField] private ComboLabel label;
		[SerializeField] [HideInInspector] private ComboElement[] elements;
		
		private IReadOnlyList<ComboData> m_dataPoints;
		private Elements.ComboBox m_parent;
		private IComboBoxFinished m_onFinished;

		public Elements.ComboBox Parent => m_parent;
		
		/*
		Root:

		Top:
		Pivot: 1y
		Position: Center+HalfSize
		Label: First

		Bottom:
		Pivot: 0y
		Position: Center-HalfSize
		Label: Last

		Inside:

		Left:
		Min: 0x
		Max: 0x
		Pivot: 1x

		Right:
		Min: 1x
		Max: 1x
		Pivot: 0x

		Top:
		Min: 1y
		Max: 1y
		Pivot: 1y

		Bottom:
		Min: 0y
		Max: 0y
		Pivot: 0y
		*/
		
		public void OnSelected()
		{
		}

		public void OnDeselected()
		{
			if (m_onFinished != null) m_onFinished.OnComboBoxFinished();
		}
		
		public void OpenElements(ComboPanelInput input)
		{
			m_parent = input.Parent;
			m_dataPoints = m_parent.DataProvider.DataPoints;
			m_onFinished = input.OnComboBoxFinished;
			var parentSize = m_parent.PanelSize;
			var parentPosition = m_parent.PanelPosition;
			var rootPosition = parentPosition + new Vector2(0f, parentSize.y / 2f);
			rectTransform.sizeDelta = parentSize;
			var currentData = m_parent.CurrentData;
			label.Initialize(currentData.Label, parentSize);
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.Clear();
			var elementsLength = elements.Length;
			var dataLength = m_dataPoints.Count;
			if (dataLength > elementsLength) throw new Exception("Too much data for now");
			for (var i = 0; i < dataLength; i++)
			{
				var element = elements[i];
				var data = m_dataPoints[i];
				if (layoutParent) layoutParent.AddLast(element.LayoutParent);
				element.gameObject.SetActive(true);
				element.Initialize(data, parentSize);
			}
			
			for (var i = elementsLength - 1; i >= dataLength; i--) DisableElement(elements[i]);
			var canvasRect = UIManager.Instance.CanvasRect;
			var fullHeight = parentSize.y * dataLength;
			var inverted = false;
			const float bottom = 0f;
			var bottomExtrusion = rootPosition.y - fullHeight;
			if (bottomExtrusion < bottom)
			{
				bottomExtrusion = bottom - bottomExtrusion;
				inverted = true;
				rootPosition = parentPosition - new Vector2(0f, parentSize.y / 2f);
				var top = canvasRect.sizeDelta.y;
				var topExtrusion = rootPosition.y + fullHeight;
				if (topExtrusion > top)
				{
					topExtrusion -= top;
					if (topExtrusion > bottomExtrusion) inverted = false;
				}
			}

			var vector = rectTransform.pivot;
			var parentTransform = layoutParent?.transform as RectTransform;
			float value;
			if (inverted)
			{
				value = 0f;
				rootPosition = parentPosition - new Vector2(0f, parentSize.y / 2f);
				label.transform.SetAsLastSibling();
			}
			else
			{
				value = 1f;
				rootPosition = parentPosition + new Vector2(0f, parentSize.y / 2f);
				label.transform.SetAsFirstSibling();
			}

			vector.y = value;
			rectTransform.pivot = vector;
			if (parentTransform)
			{
				vector = parentTransform.pivot;
				vector.y = value;
				parentTransform.pivot = vector;
				vector = parentTransform.anchorMin;
				vector.y = value;
				parentTransform.anchorMin = vector;
				vector = parentTransform.anchorMax;
				vector.y = value;
				parentTransform.anchorMax = vector;
				parentTransform.anchoredPosition = Vector2.zero;
			}
		
			rectTransform.anchoredPosition = rootPosition;
		}

		public void DisposeElements()
		{
			var dataLength = m_dataPoints.Count;
			for (var i = 0; i < dataLength; i++) DisableElement(elements[i]);
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.Clear();
			m_dataPoints = null;
			m_parent = null;
			m_onFinished = null;
		}

		internal void ElementSelected(ComboElement element)
		{
			if (m_onFinished != null) m_onFinished.OnComboBoxFinished(element.Data);
		}

		private void DisableElement(ComboElement element)
		{
			element.gameObject.SetActive(false);
			element.DisposeData();
		}

		public void OnPrewarm()
		{
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.SetCallback(this);
		}
#if UNITY_EDITOR
		public ComboElement[] Elements => elements;
		public IComboBoxFinished OnFinished => m_onFinished;
		
		private HierarchyTester<ComboElement> m_tester;
		
		public void OnHierarchyChanged()
		{
			var layoutParent = LayoutParent;
			if (layoutParent) m_tester.PerformTest(this, ref elements, layoutParent.transform, OnRemoved, OnAssigned);
		}

		private void OnAssigned(ComboElement element) => element.SetParent(this);

		private void OnRemoved(ComboElement element) => element.SetParent(null);
#endif
	}
}