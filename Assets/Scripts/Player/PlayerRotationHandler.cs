using Attributes;
using Managers.Persistent;
using Runtime.FrameUpdate;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
	[AddComponentMenu("Player/Player Rotation Handler")]
	public sealed class PlayerRotationHandler : BaseBehaviour, IFrameUpdatable
	{
		[SerializeField] private PlayerInputHandler inputHandler;
		[CanBeNullInPrefab, SerializeField] private CinemachinePanTilt panTilt;

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
			SetClampedValue(ref panTilt.PanAxis, lookAxis.x * cameraSensitivityX);
			SetClampedValue(ref panTilt.TiltAxis, -lookAxis.y * cameraSensitivityY);
			var eulerAngles = transform.eulerAngles;
			eulerAngles.y = panTilt.PanAxis.Value;
			transform.eulerAngles = eulerAngles;
		}
		
		private void SetClampedValue(ref InputAxis axis, float change) => 
			axis.Value = axis.ClampValue(axis.Value + change);
		
		private void Start() => GameManager.Instance.AddFrameUpdate(this);

		private void OnDestroy() => GameManager.Instance?.RemoveFrameUpdate(this);
	}
}