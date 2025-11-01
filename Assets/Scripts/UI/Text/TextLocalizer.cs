using Localization;
using Localization.Text;
using Managers;
using TMPro;
using UnityEngine;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Localized Text")]
	public sealed class TextLocalizer : MonoBehaviour, IOnLocalizationUpdate
	{
		[SerializeField] private TextMeshProUGUI textMesh;
		[SerializeField] private TextObject textToDisplay;

		public void Awake() => LanguageManager.Instance.AddLocalizer(this);

		private void OnDestroy() => LanguageManager.Instance.RemoveLocalizer(this);

		public void OnLocalizationUpdate() => textMesh.text = textToDisplay.Text;
	}
}