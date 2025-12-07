using UnityEngine;

namespace Callbacks.Physics
{
	public interface IOnTriggerEnter
	{
		void OnTriggerEnterImplementation(Collider other);
	}
}