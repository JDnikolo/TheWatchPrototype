using UnityEngine;
using Utilities;

public class PhysicsTest : MonoBehaviour
{
	private Rigidbody m_rigidbody;
	
	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.AddForce(Vector3.left * 10f, ForceMode.Impulse);
	}

	private void FixedUpdate()
	{
		if (Time.fixedTime < 1f) return;
		var finalVector = Vector3.zero;
		finalVector.CorrectForDirection(Vector3.right, m_rigidbody.velocity, 0f, 10f, Time.fixedDeltaTime);
		m_rigidbody.AddForce(finalVector, ForceMode.Acceleration);
	}
}