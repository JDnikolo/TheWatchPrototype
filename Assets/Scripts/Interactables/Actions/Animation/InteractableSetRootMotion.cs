using UnityEngine;

namespace Interactables.Actions.Animation
{
    [AddComponentMenu("Interactables/Animation/Set Root Motion")]
    public class InteractableSetRootMotion : Interactable
    {
        [SerializeReference] private Animator targetAnimator;
        [SerializeField] private bool rootMotionEnabled = true;
        [SerializeField] private bool resetPosition = true;
        
        private Vector3 m_initialPosition;
        
        public override void Interact()
        {
            targetAnimator.applyRootMotion = rootMotionEnabled;
            if (resetPosition) targetAnimator.transform.localPosition = m_initialPosition;
        }

        public void Awake() => m_initialPosition = targetAnimator.transform.localPosition;
    }
}