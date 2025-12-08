using System.Collections;
using System.Collections.Generic;
using Attributes;
using Interactables.Actions.Physics;
using Interactables.Triggers;
using LookupTables;
using UnityEngine;

public class MakeObjectShoutPushable : BaseBehaviour
{
    [SerializeField] [AutoAssigned(AssignMode.Self, typeof(MeshFilter))]
    private MeshFilter meshFilter;
    
    [SerializeField] [AutoAssigned(AssignMode.Self, typeof(MeshCollider))]
    private MeshCollider meshCollider;
    
    [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Rigidbody))]
    private Rigidbody selfRigidbody;
    
    private Collider m_selfCollider;
    
    private void Start()
    {
        var mesh = meshFilter.mesh;
        if (!meshCollider)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
            gameObject.AddComponent<ColliderDestructor>();
        }
        meshCollider.convex = true;
        meshCollider.sharedMesh = mesh;
        if (!selfRigidbody)
        {
            selfRigidbody = gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<RigidBodyDestructor>();
        }
        var shoutReceiver = gameObject.AddComponent<InteractableShoutTrigger>();
        shoutReceiver.SetCollider(meshCollider);
        var onShoutPush = gameObject.AddComponent<InteractablePushSelfFromPlayer>();
        shoutReceiver.SetInteractable(onShoutPush);
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
