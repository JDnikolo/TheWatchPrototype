using Attributes;
using Callbacks.ComboBox;
using Callbacks.Layout;
using Managers;
using Managers.Persistent;
using UI.ComboBox;
using UI.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/ComboBox")]
	public sealed class ComboBox : ButtonBase, IComboBoxFinished, ILayoutInputCallback
	{
		[SerializeField] private string primaryActionName = "Primary";
		[SerializeField] private string escapeActionName = "Escape";

		[SerializeField, AutoAssigned(AssignMode.Self, typeof(TextWriter))]
		private TextWriter textWriter;

		[CanBeNullInPrefab, SerializeField, AutoAssigned(AssignMode.Parent, typeof(ComboDataProvider))]
		private ComboDataProvider dataProvider;

		private IComboBoxReceiver m_receiver;
		private InputAction m_primaryAction;
		private InputAction m_escapeAction;
		private ComboData m_currentData;
		private bool m_opened;

		public IComboDataProvider DataProvider => dataProvider;

		public Vector2 PanelPosition => RectTransform.GetCenter();
		
		public Vector2 PanelSize => RectTransform.rect.size;
		
		public ComboData CurrentData => m_currentData;

		public void SetReceiver(IComboBoxReceiver receiver) => m_receiver = receiver;
		
		public void OnInput(Vector2 axis, ref Direction input)
		{
			m_primaryAction ??= InputManager.Instance.UIMap.GetAction(primaryActionName);
			m_escapeAction ??= InputManager.Instance.UIMap.GetAction(escapeActionName);
			if (m_primaryAction.WasPressedThisFrame()) OpenPanel();
			else if (m_escapeAction.WasPressedThisFrame()) ClosePanel();
		}

		public void OnComboBoxFinished()
		{
			ClosePanelInternal();
			OnDeselected();
		}

		public void OnComboBoxFinished(ComboData data)
		{
			if (m_currentData != data) SetData(data, true);
			ClosePanel();
		}

		protected override void OnClick(int clicks) => OpenPanel();

		private void OpenPanel()
		{
			if (m_opened) return;
			m_opened = true;
			enabled = false;
			ComboManager.Instance.OpenComboPanel(new ComboPanelInput(this, this));
		}

		private void ClosePanel()
		{
			if (!m_opened) return;
			ComboManager.Instance.CloseComboPanel();
			ClosePanelInternal();
		}

		private void ClosePanelInternal()
		{
			m_opened = false;
			enabled = true;
		}

		private void SetData(ComboData data, bool callback)
		{
			m_currentData = data;
			textWriter.WriteText(data.Label.Text);
			if (callback && m_receiver != null) m_receiver.OnComboBoxSelectionChanged(m_currentData);
		}

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.SetInputCallback(this);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SetData(dataProvider.CurrentData, false);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_receiver = null;
		}
	}
}