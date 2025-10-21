using Logic;
using UnityEngine;

namespace Localization.Dialogue
{
	[CreateAssetMenu(fileName = "OptionLogicCondition", menuName = "Localization/Dialogue/Conditions/Logic condition")]
	public sealed class DialogueOptionConditionLogic : DialogueOptionCondition
	{
		[SerializeField] private LogicGate logicGate;

		public override bool IsSelectable() => logicGate.Evaluate();
	}
}