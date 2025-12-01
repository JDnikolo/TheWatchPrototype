using Logic.Boolean;
using UnityEngine;

namespace Interactables.Actions.Logic
{
    [AddComponentMenu("Interactables/Logic/Logic Gate If Else")]
    public sealed class InteractableIfElse : Interactable
    {
        
        [SerializeField] private LogicBoolean logicGate;
        [SerializeField] private Interactable interactableIfTrue;
        [SerializeField] private Interactable interactableElse;

        public override void Interact()
        {
            if (logicGate.Evaluate())
            {
                interactableIfTrue?.Interact();
            }
            else
            {
                interactableElse?.Interact();
            }
        }
    }
}