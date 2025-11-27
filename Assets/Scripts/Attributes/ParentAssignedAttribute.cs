using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ParentAssignedAttribute : PropertyAttribute
	{
		public readonly Type FieldType;

		public ParentAssignedAttribute(Type fieldType) => FieldType = fieldType;
	}
}