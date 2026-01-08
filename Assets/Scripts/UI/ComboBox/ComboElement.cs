using Attributes;
using Callbacks.Layout;
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
		[CanBeNullInPrefab, SerializeField] [DisableInInspector]
		private ComboPanel comboParent;
		
		[SerializeField] private InputActionReference inputReference;

		[SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(TextWriter))]
		private TextWriter textWriter;

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
			if (inputReference.action.WasPressedThisFrame()) OnClick(0);
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