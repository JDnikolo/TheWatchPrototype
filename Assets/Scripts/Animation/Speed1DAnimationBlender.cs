using UnityEngine;

namespace Animation
{
    [AddComponentMenu("Animation/Animation Blending/Speed 1D Blender")]
    public class Speed1DAnimationBlender : AnimationBlender
    {
        [SerializeField] private Rigidbody rootRigidbody;
        
        [SerializeField] private string idleAnimationBlendName = "Blend";
        
        private int m_blend;
        private float m_maxSpeed = 1.0f;
        
        public override void SetBlendValues()
        {
            animator.SetFloat(m_blend, rootRigidbody.velocity.magnitude / m_maxSpeed);
        }
        private void Awake()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
            m_blend = Animator.StringToHash(idleAnimationBlendName);
        }
        private void OnValidate()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
        }
    }
}