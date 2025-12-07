using UnityEngine;

namespace Callbacks.Physics
{
	public interface IOnTriggerExit
	{
		void OnTriggerExitImplementation(Collider other);
	}
}