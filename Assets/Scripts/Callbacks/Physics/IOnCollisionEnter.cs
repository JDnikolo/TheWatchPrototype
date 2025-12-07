using UnityEngine;

namespace Callbacks.Physics
{
	public interface IOnCollisionEnter
	{
		void OnCollisionEnterImplementation(Collision collision);
	}
}