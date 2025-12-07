using Attributes;
using UnityEngine;

namespace LookupTables
{
    [AddComponentMenu("Lookup Tables/Rigid Body Destructor")]
    public sealed class RigidBodyDestructor : BaseBehaviour
    {
        [SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced, typeof(Rigidbody))] 
        private Rigidbody target;
        
        private void OnDestroy() => RigidBodyTable.Instance?.Remove(target);
    }
}