using UnityEngine;

namespace Logic.String
{
	[CreateAssetMenu(fileName = "Constant", menuName = "Logic/String/Constant")]
	public sealed class LogicStringConstant : LogicString
	{
		[SerializeField] private string constantValue;
		
		public override string Evaluate() => constantValue;
	}
}