using UnityEngine;

namespace Attributes
{
	public sealed class DisableInInspectorAttribute : PropertyAttribute
	{
		public bool AllowInEditor;

		public DisableInInspectorAttribute(bool allowInEditor = false) => AllowInEditor = allowInEditor;
	}
}