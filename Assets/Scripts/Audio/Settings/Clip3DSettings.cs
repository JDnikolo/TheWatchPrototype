using UnityEngine;

namespace Audio.Settings
{
	[CreateAssetMenu(fileName = "3D", menuName = "Audio/Settings/3D")]
	public sealed class Clip3DSettings : ClipSettings
	{
		[SerializeField] [Range(0f, 5f)] private float dopplerLevel = 1f;
		[SerializeField] [Range(0f, 360f)] private int spread = 0;
		
		public override void Apply(AudioSource source)
		{
			base.Apply(source);
			source.spatialBlend = 1f;
			source.dopplerLevel = dopplerLevel;
			source.spread = spread;
		}
	}
}