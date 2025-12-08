using System.Collections.Generic;
using Attributes;
using Managers.Persistent;
using Runtime;
using Runtime.FrameUpdate;
using UI.ComboBox;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Managers
{
	[AddComponentMenu("Managers/ComboBox Manager")]
	public sealed class ComboManager : Singleton<ComboManager>, IFrameUpdatable
	{
		[CanBeNullInPrefab, SerializeField] private ComboPanel comboPanel;

		private List<RaycastResult> m_results = new();
		private LayoutManager.State m_state;
		private ComboPanelInput? m_input;
		
		protected override bool Override => true;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.ComboManager;

		public void OnFrameUpdate()
		{
			if (m_input.HasValue)
			{
				if (InputManager.IsPointerPressed) return;
				OpenComboPanelInternal(m_input.Value);
				m_input = null;
				return;
			}
			
			if (!comboPanel.gameObject.activeSelf || !InputManager.WasPointerReleasedThisFrame) return;
			var uiManager = UIManager.Instance;
			uiManager.Raycaster.Raycast(
				new PointerEventData(EventSystem.current) {position = InputManager.PointerPosition}, m_results);
			//We clicked elsewhere so we want to close it
			if (m_results.Count == 0 || m_results[0].gameObject.transform.GetDeferredComponent<IComboHook>() == null) 
				CloseComboPanel();
			m_results.Clear();
		}
		
		public void OpenComboPanel(ComboPanelInput input, bool fromClick)
		{
			GameManager.Instance.AddFrameUpdate(this);
			if (fromClick) m_input = input;
			else OpenComboPanelInternal(input);
		}

		private void OpenComboPanelInternal(ComboPanelInput input)
		{
			comboPanel.OpenElements(input);
			var layoutParent = comboPanel.LayoutParent;
			if (layoutParent)
			{
				var layoutManager = LayoutManager.Instance; 
				m_state = layoutManager.PauseState;
				layoutManager.ForceSelect(layoutParent);
			}
		}
		
		public void CloseComboPanel()
		{
			GameManager.Instance.RemoveFrameUpdate(this);
			LayoutManager.Instance.PauseState = m_state;
			comboPanel.DisposeElements();
		}
	}
}