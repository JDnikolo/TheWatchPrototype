using Attributes;
using Logic.Boolean;
using UnityEngine;

namespace Interactables.Actions.Logic
{
    [AddComponentMenu("Interactables/Logic/Logic Gate If Else")]
    public sealed class InteractableIfElse : Interactable
    {
        [CanBeNullInPath("DialogueBuildingBlocks"), SerializeField]
        private LogicBoolean logicGate;

        [CanBeNullInPath("DialogueBuildingBlocks"), SerializeField]
        private Interactable interactableIfTrue;

        [CanBeNullInPath("DialogueBuildingBlocks"), SerializeField]
        private Interactable interactableElse;

        public override void Interact()
        {
            if (logicGate.Evaluate()) interactableIfTrue.Interact();
            else interactableElse.Interact();
        }
    }
}