using UnityEngine;

namespace Physics
{
	public interface IOnTriggerExit
	{
		void OnTriggerExitImplementation(Collider other);
	}
}