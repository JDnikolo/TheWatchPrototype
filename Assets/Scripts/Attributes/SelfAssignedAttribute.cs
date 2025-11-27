using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class SelfAssignedAttribute : PropertyAttribute
	{
		public readonly Type FieldType;

		public SelfAssignedAttribute(Type fieldType) => FieldType = fieldType;
	}
}