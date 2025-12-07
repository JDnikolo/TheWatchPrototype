using Attributes;
using Localization.Text;
using UI.Text;
using UnityEngine;

namespace UI.ComboBox
{
	[AddComponentMenu("UI/Elements/ComboBox/ComboBox Label")]
	public sealed class ComboLabel : BaseBehaviour, IComboHook
	{
		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(RectTransform))]
		private RectTransform rectTransform;
		[SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(TextWriter))]
		private TextWriter textWriter;
		
		public void Initialize(TextObject text, Vector2 size)
		{
			textWriter.WriteText(text?.Text);
			rectTransform.sizeDelta = size;
		}
	}
}