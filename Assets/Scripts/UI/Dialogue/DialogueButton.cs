using System;
using Localization.Dialogue;
using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities;

namespace UI.Dialogue
{
	[AddComponentMenu("UI/Dialogue/Dialogue Button")]
	public class DialogueButton : BaseBehaviour, IFrameUpdatable
	{
		[SerializeField] private ElementColor color;
		[Header("Input")] [SerializeField] private RectTransform outerCircleRect;
		[SerializeField] private RectTransform innerCircleRect;
		[SerializeField] private string selectActionName = "SelectDialogue";
		[SerializeField] private int split;
		[SerializeField] private int part;

		[Header("Fields")] [SerializeField] private Image image;
		[SerializeField] private DialogueWriter parent;

		private InputAction m_selectAction;
		private DialogueOption m_option;
		private bool m_optionEnabled;
		private bool m_hovering;
		private bool m_selected;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.GameUI;

		public void AssignDialogueOption(DialogueOption option)
		{
			m_option = option;
			m_optionEnabled = m_option && m_option.Selectable;
			if (m_optionEnabled)
			{
				GameManager.Instance.AddFrameUpdate(this);
				color.ApplyEnabled(image);
			}
			
			else color.ApplyDisabled(image);
			ResetButton();
		}

		public void ResetDialogueOption()
		{
			GameManager.Instance.RemoveFrameUpdate(this);
			m_option = null;
			m_optionEnabled = false;
			ResetButton();
		}

		private void ResetButton()
		{
			m_selected = false;
			m_hovering = false;
		}

		public void OnFrameUpdate()
		{
			if (!m_optionEnabled) return;
			var hovering = IsMouseOver(InputManager.PointerPosition);
			if (m_hovering != hovering)
			{
				m_hovering = hovering;
				if (hovering) parent.DisplayOption(m_option);
				else parent.ResetOption(m_option);
			}

			//Mouse was just pressed
			var selectAction = m_selectAction ??= InputManager.Instance.UIMap.GetAction(selectActionName);
			if (selectAction.WasPressedThisFrame()) m_selected = hovering;
			//Mouse was just released
			else if (selectAction.WasReleasedThisFrame())
			{
				if (m_selected) parent.SelectOption(m_option);
				m_selected = false;
			}
			
			if (m_selected) color.ApplyPressed(image);
			else if (hovering) color.ApplySelected(image);
			else color.ApplyEnabled(image);
		}

		private bool IsMouseOver(Vector2 mousePosition)
		{
			var pos = transform.position;
			var circleCenter = new Vector2(pos.x, pos.y);
			//BottomLeft
			var centerToMouse = mousePosition - circleCenter;
			var fromCenterSqr = Vector2.SqrMagnitude(centerToMouse);
			var innerRadius = innerCircleRect.rect.width / 2f;
			var outerRadius = outerCircleRect.rect.width / 2f;
			if (fromCenterSqr < innerRadius.Squared() || fromCenterSqr > outerRadius.Squared()) return false;
			switch (split)
			{
				//Single button, the whole counts
				case 1:
					return true;
				//Two buttons, two halves
				case 2:
					switch (part)
					{
						//Left half
						case 0:
							if (centerToMouse.x < 0) return true;
							break;
						//Right half
						case 1:
							if (centerToMouse.x > 0) return true;
							break;
					}

					break;
				//Three buttons, three parts
				case 3:
					//Negative because counter clockwise rotation
					var splitAngle = -360f / split;
					//We start with unit vectors because of perpendicular later in the equation
					var startVector = Vector2.down.Rotate(splitAngle * part * Mathf.Deg2Rad);
					var endVector = Vector2.down.Rotate(splitAngle * (part + 1) * Mathf.Deg2Rad);
					//For the mouse to be in a correct position
					//Mouse has to be locally left of the start vector
					//And locally right of the end vector
					//In the final calculation, we need the real start/end vectors, so we transform to global space
					//Then we compare the tmvector of both against their perpendicular counterparts
					if (Vector2.Dot(Vector2.Perpendicular(startVector),
							mousePosition - (circleCenter + startVector * outerRadius)) < 0 &&
						Vector2.Dot(Vector2.Perpendicular(endVector),
							mousePosition - (circleCenter + endVector * outerRadius)) > 0)
						return true;
					break;
				//Four buttons, four quarters
				case 4:
					switch (part)
					{
						//Bottom left
						case 0:
							if (centerToMouse.x < 0 && centerToMouse.y < 0) return true;
							break;
						//Top left
						case 1:
							if (centerToMouse.x < 0 && centerToMouse.y > 0) return true;
							break;
						//Top right
						case 2:
							if (centerToMouse.x > 0 && centerToMouse.y > 0) return true;
							break;
						//Bottom right
						case 3:
							if (centerToMouse.x > 0 && centerToMouse.y < 0) return true;
							break;
					}

					break;
			}

			return false;
		}

		private void OnEnable() => color.ApplyEnabled(image);

		private void OnDisable() => color.ApplyDisabled(image);
#if UNITY_EDITOR
		private void OnValidate()
		{
			if (color && image) color.Validate(image, enabled);
		}
#endif
	}
}