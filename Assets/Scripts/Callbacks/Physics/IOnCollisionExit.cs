using UnityEngine;

namespace Callbacks.Physics
{
	public interface IOnCollisionExit
	{
		void OnCollisionExitImplementation(Collision other);
	}
}