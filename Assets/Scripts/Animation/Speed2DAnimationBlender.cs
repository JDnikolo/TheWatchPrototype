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
        private float m_maxSpeed = 1.0f;
        
        public override void SetBlendValues()
        {
            var forwardSpeed = Vector3.Dot(rootRigidbody.velocity, lookDirectionTransform.forward);
            var rightSpeed = Vector3.Dot(rootRigidbody.velocity, lookDirectionTransform.right);
            
            animator.SetFloat(m_blendForward, forwardSpeed / m_maxSpeed);
            animator.SetFloat(m_blendRight, rightSpeed / m_maxSpeed);
        }
        private void Awake()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
            m_blendForward = Animator.StringToHash(idleForwardParameterName);
            m_blendRight = Animator.StringToHash(idleRightParameterName);
        }
        private void OnValidate()
        {
            m_maxSpeed = rootRigidbody.maxLinearVelocity;
        }
    }
}