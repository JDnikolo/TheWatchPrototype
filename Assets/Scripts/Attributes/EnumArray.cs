using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class EnumArray : PropertyAttribute
	{
		public readonly Type EnumType;

		public EnumArray(Type enumType) => EnumType = enumType;
	}
}