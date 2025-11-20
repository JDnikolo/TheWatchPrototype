using Runtime;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Player Manager")]
	public sealed class PlayerManager : Singleton<PlayerManager>
	{
		[SerializeField] private GameObject playerObject;
		[SerializeField] private Camera playerCamera;		
		
		protected override bool Override => true;
		
		public GameObject PlayerObject => playerObject;
		
		public Camera PlayerCamera => playerCamera;
	}
}