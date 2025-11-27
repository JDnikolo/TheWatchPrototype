using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class DeferredEditorAttribute : PropertyAttribute
	{
		public bool Disabled;

		public DeferredEditorAttribute(bool disabled = false) => Disabled = disabled;
	}
}