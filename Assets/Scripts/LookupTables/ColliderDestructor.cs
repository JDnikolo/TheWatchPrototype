using Attributes;
using UnityEngine;

namespace LookupTables
{
    [AddComponentMenu("Lookup Tables/Collider Destructor")]
    public sealed class ColliderDestructor : BaseBehaviour
    {
        [SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced | AssignMode.Child, typeof(Collider))] 
        private Collider target;
        
        private void OnDestroy() => ColliderTable.Instance?.Remove(target);
    }
}