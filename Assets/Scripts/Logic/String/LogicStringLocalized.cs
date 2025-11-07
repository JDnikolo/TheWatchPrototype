using Localization.Text;
using UnityEngine;

namespace Logic.String
{
	[CreateAssetMenu(fileName = "Localized", menuName = "Logic/String/Localized Constant")]
	public sealed class LogicStringLocalized : LogicString
	{
		[SerializeField] private TextObject localizedText;
		
		public override string Evaluate() => localizedText.Text;
	}
}