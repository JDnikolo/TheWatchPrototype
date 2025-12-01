using Animation;
using UnityEngine;

namespace Interactables.Actions.Animation
{
    [AddComponentMenu("Interactables/Animation/Start Default Animation")]
    public class InteractableStartDefaultAnimation : Interactable
    {
        [SerializeReference] private PersonAnimationController personAnimationController;
        [SerializeField] private DefaultAnimations animationToStart;
        [SerializeField] private float duration = -1.0f;
        [SerializeField] private Interactable onAnimationEnd = null;
        public override void Interact()
        {
            var animationHash = animationToStart switch
            {
                DefaultAnimations.Salute => DefaultAnimationHash.Salute,
                DefaultAnimations.Scared => DefaultAnimationHash.Scared,
                DefaultAnimations.Talking => DefaultAnimationHash.Talking,
                DefaultAnimations.Waving => DefaultAnimationHash.Waving,
                _ => 0
            };
            if (animationHash == 0) return;
            personAnimationController.StartAnimation(animationHash, duration, callback:onAnimationEnd);
        }
    }
}