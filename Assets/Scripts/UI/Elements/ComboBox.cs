using Attributes;
using Callbacks.Backing;
using Callbacks.ComboBox;
using Callbacks.Layout;
using Input;
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
	public sealed class ComboBox : ButtonBase, IComboBoxFinished, ILayoutInputCallback, IBackHook
	{
		[SerializeField] private InputActionReference inputReference;

		[SerializeField, AutoAssigned(AssignModeFlags.Self, typeof(TextWriter))]
		private TextWriter textWriter;

		[CanBeNullInPrefab, SerializeField, AutoAssigned(AssignModeFlags.Parent, typeof(ComboDataProvider))]
		private ComboDataProvider dataProvider;

		private IComboBoxReceiver m_receiver;
		private ComboData m_currentData;
		private bool m_opened;
		private bool m_fromClick;

		public IComboDataProvider DataProvider => dataProvider;

		public Vector2 PanelPosition => RectTransform.GetCenter();
		
		public Vector2 PanelSize => RectTransform.rect.size;
		
		public ComboData CurrentData => m_currentData;

		private bool Opened
		{
			get => m_opened;
			set
			{
				if (m_opened == value) return;
				m_opened = value;
				if (value) InputManager.Instance?.BackSpecial.AddHook(this);
				else InputManager.Instance?.BackSpecial.RemoveHook(this);
			}
		}

		protected override void Deselect()
		{
			base.Deselect();
			Debug.Log("Deselect");
		}

		public void SetReceiver(IComboBoxReceiver receiver) => m_receiver = receiver;
		
		public void OnInput(Vector2 axis, ref Direction input)
		{
			if (inputReference.action.WasPressedThisFrame()) OpenPanel(false);
		}
		
		public void OnBackPressed(InputState inputState)
		{
			if (inputState == InputState.Pressed) ClosePanel();
		}

		public void OnComboBoxFinished() => ClosePanelInternal();

		public void OnComboBoxFinished(ComboData data)
		{
			if (m_currentData != data) SetData(data, true);
			ClosePanel();
		}

		protected override void OnClick(int clicks) => OpenPanel(clicks >= 0);

		private void OpenPanel(bool fromClick)
		{
			if (Opened) return;
			Opened = true;
			enabled = false;
			m_fromClick = fromClick;
			GameManager.Instance.InvokeOnNextFrameUpdate(OpenDelayed);
		}

		private void OpenDelayed() => ComboManager.Instance.OpenComboPanel(
			new ComboPanelInput(this, this), m_fromClick);

		private void ClosePanel()
		{
			if (!Opened) return;
			ComboManager.Instance.CloseComboPanel();
			ClosePanelInternal();
		}

		private void ClosePanelInternal()
		{
			Opened = false;
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
			InputManager.Instance?.BackSpecial.RemoveHook(this);
		}
#if UNITY_EDITOR
		public bool EditorOpened => Opened;
#endif
	}
}