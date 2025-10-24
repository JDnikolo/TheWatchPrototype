using UnityEngine;
using Variables;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Increase Number Interactable")]
    public sealed class InteractableNumberAdd : Interactable
    {
        [SerializeField] private NumberVariable variable;
        [SerializeField] private int numberToAdd = 1;

        public override void Interact() => variable.Value += numberToAdd;
    }
}