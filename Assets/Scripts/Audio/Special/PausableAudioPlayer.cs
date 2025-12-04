using Callbacks.Pausing;
using Managers;
using UnityEngine;

namespace Audio.Special
{
	[AddComponentMenu("Audio/Pausable Audio Player")]
	public sealed class PausableAudioPlayer : AudioPlayer, IPauseCallback
	{
		public void OnPauseChanged(bool paused)
		{
			if (paused) Pause();
			else UnPause();
		}

		private void Start() => PauseManager.Instance.AddPausedCallback(this);

		protected override void OnDestroy()
		{
			base.OnDestroy();
			PauseManager.Instance?.RemovePausedCallback(this);
		}
	}
}