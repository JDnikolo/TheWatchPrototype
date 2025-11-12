using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
	[AddComponentMenu("Scripts/Managers/Audio Manager")]
	public sealed class AudioManager : Singleton<AudioManager>
	{
		[SerializeField] private AudioMixer mixer;
		
		protected override bool Override => false;
	}
}