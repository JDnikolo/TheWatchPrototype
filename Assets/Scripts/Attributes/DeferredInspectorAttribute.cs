using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class DeferredInspectorAttribute : PropertyAttribute
	{
	}
}