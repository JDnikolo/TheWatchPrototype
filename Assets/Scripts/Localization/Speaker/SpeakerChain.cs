using UnityEngine;

namespace Localization.Speaker
{
	[CreateAssetMenu(fileName = "SpeakerChain", menuName = "Localization/Speaker/Speaker Chain")]
	public sealed class SpeakerChain : BaseObject
	{
		//[MinCount(2)] 
		[SerializeField] private SpeakerObject[] textAssets;
		
		public SpeakerObject[] TextAssets => textAssets;
	}
}