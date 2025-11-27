using System.Collections.Generic;
using Managers.Persistent;
using Runtime;
using Runtime.FrameUpdate;
using UI.ComboBox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Managers
{
	[AddComponentMenu("Managers/ComboBox Manager")]
	public sealed class ComboManager : Singleton<ComboManager>, IFrameUpdatable
	{
		[SerializeField] private ComboPanel comboPanel;
		[SerializeField] private Transform comboRoot;

		private List<RaycastResult> m_results = new();
		private LayoutManager.State m_state;
		private InputAction m_primaryAction;

		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.ComboManager;

		public void OnFrameUpdate()
		{
			if (!comboPanel.gameObject.activeSelf || !InputManager.WasPointerPressedThisFrame) return;
			var uiManager = UIManager.Instance;
			uiManager.Raycaster.Raycast(
				new PointerEventData(EventSystem.current) {position = InputManager.PointerPosition}, m_results);
			//We clicked elsewhere so we want to close it
			if (m_results.Count == 0 || !m_results[0].gameObject.GetComponent<ComboElement>()) CloseComboPanel();
			m_results.Clear();
		}
		
		public void OpenComboPanel(ComboPanelInput input)
		{
			comboPanel.OpenElements(input);
			var layoutParent = comboPanel.LayoutParent;
			if (layoutParent)
			{
				var layoutManager = LayoutManager.Instance; 
				m_state = layoutManager.PauseState;
				layoutManager.ForceSelect(layoutParent);
			}
			
			GameManager.Instance.AddFrameUpdate(this);
		}
		
		public void CloseComboPanel()
		{
			GameManager.Instance.RemoveFrameUpdate(this);
			LayoutManager.Instance.PauseState = m_state;
			comboPanel.DisposeElements();
		}
	}
}