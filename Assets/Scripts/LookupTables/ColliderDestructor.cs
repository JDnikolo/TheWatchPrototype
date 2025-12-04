using Attributes;
using Runtime.Automation;
using UnityEngine;

namespace LookupTables
{
    [AddComponentMenu("Lookup Tables/Collider Destructor")]
    public sealed class ColliderDestructor : MonoBehaviour
    {
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Collider))] 
        private Collider target;
        
        private void OnDestroy() => ColliderTable.Instance?.Remove(target);
    }
}