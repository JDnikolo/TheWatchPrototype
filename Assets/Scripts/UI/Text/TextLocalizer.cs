using Localization;
using Localization.Text;
using Managers;
using TMPro;
using UnityEngine;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Localized Text")]
	public sealed class TextLocalizer : MonoBehaviour, IOnLocalizationUpdate, IStartable
	{
		[SerializeField] private TextMeshProUGUI textMesh;
		[SerializeField] private TextObject textToDisplay;

		public byte StartOrder => 0;

		public void OnStart() => LanguageManager.Instance.AddLocalizer(this);

		private void OnDestroy() => LanguageManager.Instance.RemoveLocalizer(this);

		public void OnLocalizationUpdate() => textMesh.text = textToDisplay.Text;
	}
}