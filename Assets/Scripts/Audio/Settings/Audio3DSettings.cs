using UnityEngine;

namespace Audio.Settings
{
	[CreateAssetMenu(fileName = "3D", menuName = "Audio/Settings/3D")]
	public sealed class Audio3DSettings : AudioSettings
	{
		[SerializeField] [Range(0f, 5f)] private float dopplerLevel = 1f;
		[SerializeField] [Range(0f, 360f)] private int spread;
		
		public override void Apply(AudioSource source, AudioGroup group)
		{
			base.Apply(source, group);
			source.spatialBlend = 1f;
			source.dopplerLevel = dopplerLevel;
			source.spread = spread;
		}
	}
}