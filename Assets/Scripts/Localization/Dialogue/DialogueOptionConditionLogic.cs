using Logic.Boolean;
using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "OptionLogicCondition", menuName = "Localization/Dialogue/Conditions/Logic condition")]
	public sealed class DialogueOptionConditionLogic : DialogueOptionCondition
	{
		[SerializeField] private LogicBoolean logicGate;

		public override bool IsSelectable() => logicGate.Evaluate();
	}
}