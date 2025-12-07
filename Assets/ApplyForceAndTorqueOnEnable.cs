using System.Collections;
using System.Collections.Generic;
using Attributes;
using Runtime.Automation;
using UnityEngine;

public class ApplyForceAndTorqueOnEnable : MonoBehaviour
{
    [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Rigidbody))]
    private Rigidbody rigidbody;
    [SerializeField] private Vector3 forceVector = new Vector3(0, 10, 0);
    [SerializeField] private Vector3 torqueVector = new Vector3(0, 1, 1);
    public void OnEnable()
    {
        rigidbody.AddForce(forceVector, ForceMode.Impulse);
        rigidbody.AddTorque(torqueVector, ForceMode.Impulse);
    }

}
