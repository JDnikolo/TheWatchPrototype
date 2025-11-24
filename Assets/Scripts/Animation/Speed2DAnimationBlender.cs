using UnityEngine;

namespace Animation
{
    [AddComponentMenu("Animation/Animation Blending/Speed 2D Blender")]
    public class Speed2DAnimationBlender : AnimationBlender
    {
        [SerializeField] private Rigidbody rootRigidbody;
        [SerializeReference] private Transform lookDirectionTransform;
        
        [SerializeField] private string idleForwardParameterName = "SpeedForward";
        [SerializeField] private string idleRightParameterName = "SpeedRight";
        
        private int m_blendForward, m_blendRight;
        //You sure this isnt to be used?
        private float m_maxSpeed;
        
        public override void SetBlendValues()
        {
            var forwardSpeed = Vector3.Dot(rootRigidbody.velocity.normalized, lookDirectionTransform.forward.normalized);
            var rightSpeed = Vector3.Dot(rootRigidbody.velocity.normalized, lookDirectionTransform.right.normalized);
            animator.SetFloat(m_blendForward, forwardSpeed);
            animator.SetFloat(m_blendRight, rightSpeed);
        }
        
        private void Awake()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
            m_blendForward = Animator.StringToHash(idleForwardParameterName);
            m_blendRight = Animator.StringToHash(idleRightParameterName);
        }
    }
}