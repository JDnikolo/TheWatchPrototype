using UnityEngine;

namespace Animation
{
    public abstract class AnimationBlender : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        
        public abstract void SetBlendValues();
    }
}