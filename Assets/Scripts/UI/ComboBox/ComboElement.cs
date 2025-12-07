using Attributes;
using Callbacks.Layout;
using Managers.Persistent;
using UI.Elements;
using UI.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/ComboBox Element")]
	public sealed class ComboElement : ButtonBase, ILayoutInputCallback, IComboHook
	{
		[CanBeNullInPrefab, SerializeField] [DisableInInspector] private ComboPanel comboParent;
		[SerializeField] private string primaryActionName = "Primary";

		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(TextWriter))]
		private TextWriter textWriter;
		
		private InputAction m_primaryAction;

		public ComboData Data { get; private set; }

		public void Initialize(ComboData data, Vector2 size)
		{
			Data = data;
			textWriter.WriteText(data.Label?.Text);
			RectTransform.sizeDelta = size;
		}
		
		public void DisposeData() => Data = default;

		public void OnInput(Vector2 axis, ref Direction input)
		{
			m_primaryAction ??= InputManager.Instance.UIMap.GetAction(primaryActionName);
			if (m_primaryAction.WasPressedThisFrame()) OnClick(0);
		}
		
		protected override void OnClick(int clicks) => comboParent.ElementSelected(this);

		public override void OnPrewarm()
		{
			base.OnPrewarm();
			var layoutParent = LayoutParent;
			if (layoutParent) layoutParent.SetInputCallback(this);
		}
#if UNITY_EDITOR
		public void SetParent(ComboPanel newComboParent) => this.DirtyReplaceObject(ref comboParent, newComboParent);
#endif
	}
}