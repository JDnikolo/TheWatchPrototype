using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Camera Manager")]
	public sealed class CameraManager : Singleton<CameraManager>
	{
		[SerializeField] private Camera persistentCamera;

		private Camera m_camera;

		protected override bool Override => false;
		
		protected override void Awake()
		{
			SetPersistent();
			base.Awake();
		}

		public void SetPersistent() => SetCamera(persistentCamera);

		public void SetCamera(Camera camera)
		{
			var oldCamera = m_camera;
			m_camera = camera;
			if (oldCamera != null) oldCamera.gameObject.SetActive(false);
			if (camera != null) camera.gameObject.SetActive(true);
		}
	}
}