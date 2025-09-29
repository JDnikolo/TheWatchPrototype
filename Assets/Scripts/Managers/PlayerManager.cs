using UnityEngine;

namespace Managers
{
	public sealed class PlayerManager : Singleton<PlayerManager>
	{
		[SerializeField] private GameObject playerObject;
	}
}