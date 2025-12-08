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
        
        private int m_blendForward;
        private int m_blendRight;

        public override void SetBlendValues()
        {
            var velocity = rootRigidbody.velocity;
            var factor  = velocity.magnitude/rootRigidbody.maxLinearVelocity;
            var forwardSpeed = Vector3.Dot(velocity, lookDirectionTransform.forward) * factor;
            var rightSpeed = Vector3.Dot(velocity, lookDirectionTransform.right) * factor;
            Animator.SetFloat(m_blendForward, forwardSpeed);
            Animator.SetFloat(m_blendRight, rightSpeed);
        }
        
        private void Awake()
        {
            m_blendForward = Animator.StringToHash(idleForwardParameterName);
            m_blendRight = Animator.StringToHash(idleRightParameterName);
        }
    }
}