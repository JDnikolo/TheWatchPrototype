using UnityEngine;

namespace Localization.Speaker
{
	[CreateAssetMenu(fileName = "SpeakerChain", menuName = "Localization/Speaker/Speaker Chain")]
	public class SpeakerChain : LocalizationAsset
	{
		[SerializeField] private SpeakerObject[] textAssets;
		
		public SpeakerObject[] TextAssets => textAssets;
	}
}