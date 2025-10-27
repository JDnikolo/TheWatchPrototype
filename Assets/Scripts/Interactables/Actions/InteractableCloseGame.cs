using UnityEngine;

namespace Interactables.Actions
{
    [AddComponentMenu("Interactables/Other/Close Game Interactable")]
    public class InteractableCloseGame : Interactable
    {
        //TODO: REMOVE THIS AFTER PLAYTEST
        public override void Interact() => Application.Quit();
    }
}