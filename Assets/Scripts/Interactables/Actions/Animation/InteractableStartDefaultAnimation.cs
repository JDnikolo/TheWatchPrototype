using Animation;
using Attributes;
using UnityEngine;

namespace Interactables.Actions.Animation
{
    [AddComponentMenu("Interactables/Animation/Start Default Animation")]
    public sealed class InteractableStartDefaultAnimation : Interactable
    {
        [SerializeField] private PersonAnimationController personAnimationController;
        [SerializeField] private DefaultAnimationsEnum animationToStart;
        [SerializeField] private float duration = -1.0f;
        [CanBeNull, SerializeField] private Interactable onAnimationEnd;
        
        public override void Interact()
        {
            var animationHash = animationToStart switch
            {
                DefaultAnimationsEnum.Salute => DefaultAnimationHash.Salute,
                DefaultAnimationsEnum.Scared => DefaultAnimationHash.Scared,
                DefaultAnimationsEnum.Talking => DefaultAnimationHash.Talking,
                DefaultAnimationsEnum.Waving => DefaultAnimationHash.Waving,
                DefaultAnimationsEnum.Angry => DefaultAnimationHash.Angry,
                DefaultAnimationsEnum.Crouched => DefaultAnimationHash.Crouched,
                _ => 0
            };
            if (animationHash == 0) return;
            personAnimationController.StartAnimation(animationHash, duration, callback:onAnimationEnd);
        }
    }
}