using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	[CreateAssetMenu(fileName = "Color", menuName = "UI/Element Color")]
	public sealed class ElementColor : ScriptableObject
	{
		[SerializeField] private Color enabledColor;
		[SerializeField] private Color disabledColor;
		[SerializeField] private Color selectedColor;
		[SerializeField] private Color pressedColor;

		public void ApplyEnabled(Image image) => image.color = enabledColor;

		public void ApplyDisabled(Image image) => image.color = disabledColor;

		public void ApplySelected(Image image) => image.color = selectedColor;

		public void ApplyPressed(Image image) => image.color = pressedColor;

		public void Validate(Image image, bool enabled) => image.color = enabled ? enabledColor : disabledColor;
	}
}