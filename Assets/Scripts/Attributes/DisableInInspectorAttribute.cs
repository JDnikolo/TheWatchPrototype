using UnityEngine;

namespace Attributes
{
	public sealed class DisableInInspectorAttribute : PropertyAttribute
	{
		public bool EditorOnly;

		public DisableInInspectorAttribute(bool editorOnly = true) => EditorOnly = editorOnly;
	}
}