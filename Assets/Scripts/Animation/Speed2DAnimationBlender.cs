using UnityEngine;

namespace Animation
{
    [AddComponentMenu("Animation/Animation Blending/Speed 2D Blender")]
    public sealed class Speed2DAnimationBlender : AnimationBlender
    {
        [SerializeField] private Rigidbody rootRigidbody;
        [SerializeField] private Transform lookDirectionTransform;
        
        [SerializeField] private string idleForwardParameterName = "SpeedForward";
        [SerializeField] private string idleRightParameterName = "SpeedRight";
        
        private int m_blendForward, m_blendRight;
        //TODO Check what this is used for
        private float m_maxSpeed;
        
        public override void SetBlendValues()
        {
            var forwardSpeed = Vector3.Dot(rootRigidbody.velocity.normalized, lookDirectionTransform.forward.normalized);
            var rightSpeed = Vector3.Dot(rootRigidbody.velocity.normalized, lookDirectionTransform.right.normalized);
            Animator.SetFloat(m_blendForward, forwardSpeed);
            Animator.SetFloat(m_blendRight, rightSpeed);
        }
        
        private void Awake()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
            m_blendForward = Animator.StringToHash(idleForwardParameterName);
            m_blendRight = Animator.StringToHash(idleRightParameterName);
        }
    }
}