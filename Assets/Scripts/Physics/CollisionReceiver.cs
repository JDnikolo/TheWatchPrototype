using UnityEngine;

namespace Physics
{
	public abstract class CollisionReceiver : MonoBehaviour, ICollisionReceiver
	{
		public abstract CollisionReceiverFlags Flags { get; }
	}
}