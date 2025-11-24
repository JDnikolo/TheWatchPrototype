using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
    [AddComponentMenu("Interactables/Variables/Increase Number Variable")]
    public sealed class InteractableNumberAdd : Interactable
    {
        [SerializeField] private NumberVariable variable;
        [SerializeField] private int numberToAdd = 1;

        public override void Interact() => variable.Value += numberToAdd;
    }
}