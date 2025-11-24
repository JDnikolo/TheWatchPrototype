using TMPro;
using UnityEngine;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Text Writer")]
	public sealed class TextOnly : TextWriter, ITextMeshProvider
	{
		[SerializeField] private TextMeshProUGUI textMesh;
		
		public TextMeshProUGUI TextMesh => textMesh;
		
		public override void WriteText(string text) => textMesh.text = text;
	}
}