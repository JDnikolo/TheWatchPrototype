using Logic.Boolean;
using UnityEngine;

namespace Logic.String
{
	[CreateAssetMenu(fileName = "StringIfBoolean", menuName = "Logic/String/Boolean IF-Gate")]
	public sealed class LogicStringBoolean : LogicString
	{
		[SerializeField] private LogicBoolean logicGate;
		[SerializeField] private LogicString onTrue;
		[SerializeField] private LogicString onFalse;

		public override string Evaluate()
		{
			LogicString str;
			if (logicGate.Evaluate()) str = onTrue;
			else str = onFalse;
			return str.Evaluate();
		}
	}
}