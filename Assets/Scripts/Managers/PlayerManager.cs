using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Player Manager")]
	public sealed class PlayerManager : Singleton<PlayerManager>
	{
		[SerializeField] private GameObject playerObject;
		[SerializeField] private new Camera camera;
		
		public GameObject PlayerObject => playerObject;
		
		public Camera Camera => camera;
	}
}