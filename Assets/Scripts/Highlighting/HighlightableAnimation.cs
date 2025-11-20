using Animation;
using Interactables;
using UnityEngine;

namespace Highlighting
{
    public class HighlightableAnimation : Highlightable
    {
        [SerializeField] private PersonAnimationController animationController = null;
        [SerializeField] private string animationName = "Salute";
        [SerializeField] private float animationDuration = -1.0f;
        [SerializeField] private Interactable onAnimationEnd = null;
        [SerializeField] private bool oneShot = false;
        
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