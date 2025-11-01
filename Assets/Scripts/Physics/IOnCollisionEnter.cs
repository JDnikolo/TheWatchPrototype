using UnityEngine;

namespace Physics
{
	public interface IOnCollisionEnter
	{
		void OnCollisionEnterImplementation(Collision collision);
	}
}