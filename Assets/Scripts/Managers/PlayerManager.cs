using Attributes;
using Runtime;
using Unity.Cinemachine;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Player Manager")]
	public sealed class PlayerManager : Singleton<PlayerManager>
	{
		[CanBeNullInPrefab, SerializeField] private GameObject playerObject;
		[CanBeNullInPrefab, SerializeField] private Rigidbody playerRigidbody;
		[CanBeNullInPrefab, SerializeField] private CinemachineCamera playerCamera;

		protected override bool Override => true;

		public GameObject PlayerObject => playerObject;

		public Rigidbody PlayerRigidbody => playerRigidbody;

		public CinemachineCamera PlayerCamera => playerCamera;
	}
}