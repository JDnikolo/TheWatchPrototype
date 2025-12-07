using Managers;
using Managers.Persistent;
using Runtime.FrameUpdate;
using UnityEngine;

namespace Player
{
	[AddComponentMenu("Player/Player Rotation Handler")]
	public sealed class PlayerRotationHandler : BaseBehaviour, IFrameUpdatable
	{
		[SerializeField] private PlayerInputHandler inputHandler;

		[Header("Rotation")]
		// ReSharper disable once MissingLinebreak
		[SerializeField] private float cameraSensitivityX = Mathf.PI * 0.01f;
		[SerializeField] private float cameraSensitivityY = Mathf.PI * 0.01f;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Player;

		private float m_yawDegrees;
		private float m_pitchDegrees;
		
		public void OnFrameUpdate()
		{
			if (!inputHandler.TryGetLookAxis(out var lookAxis)) return;
			lookAxis *= Time.deltaTime;
			m_yawDegrees += lookAxis.x * cameraSensitivityX;
			while (m_yawDegrees > 360f) m_yawDegrees -= 360f;
			while (m_yawDegrees < 0f) m_yawDegrees += 360f;
			m_pitchDegrees -= lookAxis.y * cameraSensitivityY;
			if (m_pitchDegrees > 90f) m_pitchDegrees = 90f;
			else if (m_pitchDegrees < -90f) m_pitchDegrees = -90f;
			var playerManager = PlayerManager.Instance;
			var cameraTransform = playerManager.PlayerCamera.transform;
			var eulerAngles = cameraTransform.eulerAngles;
			eulerAngles.x = m_pitchDegrees;
			eulerAngles.y = m_yawDegrees;
			cameraTransform.localEulerAngles = eulerAngles;
			var playerTransform = playerManager.PlayerObject.transform;
			eulerAngles = playerTransform.eulerAngles;
			eulerAngles.y = m_yawDegrees;
			playerTransform.localEulerAngles = eulerAngles;
		}
		
		private void Start()
		{
			GameManager.Instance.AddFrameUpdate(this);
			m_yawDegrees = PlayerManager.Instance.PlayerCamera.transform.eulerAngles.y;
		}

		private void OnDestroy() => GameManager.Instance?.RemoveFrameUpdate(this);
	}
}