using UnityEngine;

namespace Physics
{
	public interface IOnTriggerEnter
	{
		void OnTriggerEnterImplementation(Collider other);
	}
}