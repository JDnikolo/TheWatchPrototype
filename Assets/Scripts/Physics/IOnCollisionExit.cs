using UnityEngine;

namespace Physics
{
	public interface IOnCollisionExit
	{
		void OnCollisionExitImplementation(Collision other);
	}
}