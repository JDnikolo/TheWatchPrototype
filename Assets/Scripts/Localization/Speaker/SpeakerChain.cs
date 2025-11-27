using UnityEngine;

namespace Localization.Speaker
{
	[CreateAssetMenu(fileName = "SpeakerChain", menuName = "Localization/Speaker/Speaker Chain")]
	public class SpeakerChain : ScriptableObject
	{
		[SerializeField] private SpeakerObject[] textAssets;
		
		public SpeakerObject[] TextAssets => textAssets;
	}
}