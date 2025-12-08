using Attributes;
using UnityEngine;

namespace Animation
{
    public sealed class ApplyForceAndTorqueOnEnable : MonoBehaviour
    {
        [SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(Rigidbody))] private new Rigidbody rigidbody;
        [SerializeField] private Vector3 forceVector = new(0, 10, 0);
        [SerializeField] private Vector3 torqueVector = new(0, 1, 1);
        public void OnEnable()
        {
            rigidbody.AddForce(forceVector, ForceMode.Impulse);
            rigidbody.AddTorque(torqueVector, ForceMode.Impulse);
        }
    }
}
