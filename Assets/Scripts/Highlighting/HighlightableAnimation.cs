using Animation;
using Interactables;
using UnityEngine;

namespace Highlighting
{
    [AddComponentMenu("Highlighting/Animation Highlightable")]
    public sealed class HighlightableAnimation : Highlightable
    {
        [SerializeField] private PersonAnimationController animationController;
        [SerializeField] private string animationName = "Salute";
        [SerializeField] private float animationDuration = -1.0f;
        [SerializeField] private Interactable onAnimationEnd;
        [SerializeField] private bool oneShot;
        
        protected override void HighlightInternal(bool enable)
        {
            if (enable)
            {
                animationController.StartAnimation(Animator.StringToHash(animationName), animationDuration, onAnimationEnd);
                if (oneShot) Destroy(this);
            }
            else
            {
                animationController.StopAnimation(Animator.StringToHash(animationName));
            }
        }
    }
}