using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class EnumArrayAttribute : PropertyAttribute
	{
		public readonly Type EnumType;

		public EnumArrayAttribute(Type enumType) => EnumType = enumType;
	}
}