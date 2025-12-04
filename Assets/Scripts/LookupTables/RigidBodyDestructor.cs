using Attributes;
using Runtime.Automation;
using UnityEngine;

namespace LookupTables
{
    [AddComponentMenu("Lookup Tables/Rigid Body Destructor")]
    public sealed class RigidBodyDestructor : MonoBehaviour
    {
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Rigidbody))] 
        private Rigidbody target;
        
        private void OnDestroy() => RigidBodyTable.Instance?.Remove(target);
    }
}