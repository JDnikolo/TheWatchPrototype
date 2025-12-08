using UnityEngine;

namespace Animation
{
    [AddComponentMenu("Animation/Animation Blending/Speed 1D Blender")]
    public sealed class Speed1DAnimationBlender : AnimationBlender
    {
        [SerializeField] private Rigidbody rootRigidbody;
        [SerializeField] private string idleAnimationBlendName = "Blend";

        private int m_blend;

        public override void SetBlendValues() => 
            Animator.SetFloat(m_blend, rootRigidbody.velocity.magnitude / rootRigidbody.maxLinearVelocity);

        private void Awake() => m_blend = Animator.StringToHash(idleAnimationBlendName);
    }
}