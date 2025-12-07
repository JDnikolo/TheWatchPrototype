using UnityEngine;

namespace Animation
{
    public abstract class AnimationBlender : BaseBehaviour
    {
        [SerializeField] private Animator animator;
        
        protected Animator Animator => animator;
        
        public abstract void SetBlendValues();
    }
}