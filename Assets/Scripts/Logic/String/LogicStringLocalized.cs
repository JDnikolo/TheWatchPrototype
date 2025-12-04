using Localization.Text;
using UnityEngine;

namespace Logic.String
{
	[CreateAssetMenu(fileName = "Localized", menuName = "Logic/String/Localized Constant")]
	public sealed class LogicStringLocalized : LogicString
	{
		[SerializeField] private TextObject initialLocalizedText;

		private TextObject m_localizedText;
		private void OnEnable() => m_localizedText = initialLocalizedText;
		
		public override string Evaluate() => m_localizedText.Text;
		
		public void SetLocalizedText(TextObject text) => m_localizedText = text;
	}
}