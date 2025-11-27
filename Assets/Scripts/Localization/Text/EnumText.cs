using UnityEngine;

namespace Localization.Text
{
	public abstract class EnumText : ScriptableObject
	{
		public abstract TextObject[] Values { get; }
	}
}