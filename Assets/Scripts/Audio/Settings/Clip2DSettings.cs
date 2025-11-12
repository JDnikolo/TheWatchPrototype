using UnityEngine;

namespace Audio.Settings
{
	[CreateAssetMenu(fileName = "2D", menuName = "Audio/Settings/2D")]
	public sealed class Clip2DSettings : ClipSettings
	{
		public override void Apply(AudioSource source)
		{
			base.Apply(source);
			source.spatialBlend = 0f;
		}
	}
}