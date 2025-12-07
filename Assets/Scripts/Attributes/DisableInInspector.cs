using UnityEngine;

namespace Attributes
{
	public sealed class DisableInInspector : PropertyAttribute
	{
		public bool AllowInEditor;

		public DisableInInspector(bool allowInEditor = false) => AllowInEditor = allowInEditor;
	}
}