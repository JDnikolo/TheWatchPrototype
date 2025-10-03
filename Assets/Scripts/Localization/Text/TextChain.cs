using UnityEngine;

namespace Localization.Text
{
	[CreateAssetMenu(fileName = "TextChain", menuName = "Localization/Text/Text Chain")]
	public class TextChain : LocalizationAsset
	{
		[SerializeField] private TextObject[] textAssets;
		
		public TextObject[] TextAssets => textAssets;
	}
}