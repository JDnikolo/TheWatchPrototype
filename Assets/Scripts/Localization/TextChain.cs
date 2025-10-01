using UnityEngine;

namespace Localization
{
	[CreateAssetMenu(fileName = "TextChain", menuName = "Localization/Text Chain")]
	public class TextChain : ScriptableObject
	{
		[SerializeField] private TextAsset[] textAssets;
		
		public TextAsset[] TextAssets => textAssets;
	}
}