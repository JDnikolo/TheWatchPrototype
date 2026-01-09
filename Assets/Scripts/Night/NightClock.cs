using Managers;
using UnityEngine;

namespace Night
{
	[AddComponentMenu("Night/Night Clock")]
	public sealed class NightClock : BaseBehaviour
	{
		[SerializeField] private Transform secondHandle;
		[SerializeField] private Transform minuteHandle;
		[SerializeField] private Transform hourHandle;

		private void OnTimeChanged(NightTime currentTime)
		{
			RotateTo(secondHandle, currentTime.second / 60f);
			RotateTo(minuteHandle, currentTime.minute / 60f);
			RotateTo(hourHandle, currentTime.hour / 24f);
		}
		
		private static void RotateTo(Transform transform, float partOfWhole)
		{
			var localEuler = transform.localEulerAngles;
			localEuler.z = -(partOfWhole * 360f);
			transform.localEulerAngles = localEuler;
		}

		private void OnEnable()
		{
			var nightManager = NightManager.Instance;
			if (nightManager) nightManager.OnTimeChanged += OnTimeChanged;
		}

		private void OnDisable()
		{
			var nightManager = NightManager.Instance;
			if (nightManager) nightManager.OnTimeChanged -= OnTimeChanged;
		}
	}
}