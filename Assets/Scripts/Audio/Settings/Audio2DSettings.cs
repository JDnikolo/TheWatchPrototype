using UnityEngine;

namespace Audio.Settings
{
	[CreateAssetMenu(fileName = "2D", menuName = "Audio/Settings/2D")]
	public sealed class Audio2DSettings : AudioSettings
	{
		public override void Apply(AudioSource source, AudioGroup group)
		{
			base.Apply(source, group);
			source.spatialBlend = 0f;
		}
	}
}