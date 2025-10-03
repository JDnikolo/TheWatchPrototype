using System;
using UnityEngine;

namespace Attributes
{
	public class EnumArrayAttribute : PropertyAttribute
	{
		public readonly Type EnumType;

		public EnumArrayAttribute(Type enumType) => EnumType = enumType;
	}
}