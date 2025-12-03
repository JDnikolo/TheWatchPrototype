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
		
		private Rigidbody m_playerRigidbody;

		public Rigidbody PlayerRigidbody
		{
			get { 
			m_playerRigidbody ??= playerObject.GetComponent<Rigidbody>();
			return m_playerRigidbody;
			}
		}
		
		public Camera PlayerCamera => playerCamera;
	}
}