using Localization;
using Localization.Text;
using Managers;
using UI.Text;
using UnityEngine;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Label")]
	public sealed class Label : MonoBehaviour, IOnLocalizationUpdate
	{
		[SerializeField] private TextWriter textWriter;
		[SerializeField] private TextObject textToDisplay;

		private void Start()
		{
			var languageManager = LanguageManager.Instance;
			if (languageManager) languageManager.AddLocalizer(this);
		}

		private void OnDestroy()
		{
			var languageManager = LanguageManager.Instance;
			if (languageManager) languageManager.RemoveLocalizer(this);
		}

		public void OnLocalizationUpdate() => textWriter.WriteText(textToDisplay.Text);
	}
}