using Attributes;
using TMPro;
using UnityEngine;

namespace UI.Text
{
	[AddComponentMenu("UI/Text/Text Writer")]
	public sealed class TextOnly : TextWriter
	{
		[SerializeField] [DeferredEditor] private TextMeshProUGUI textMesh;
		
		public override void WriteText(string text) => textMesh.text = text;
	}
}