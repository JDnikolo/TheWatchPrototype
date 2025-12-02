using System;
using Runtime.Automation;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AutoAssignedAttribute : PropertyAttribute
	{
		public readonly AssignMode AssignMode;
		public readonly Type FieldType;

		public AutoAssignedAttribute(AssignMode assignMode, Type fieldType)
		{
			AssignMode = assignMode;
			FieldType = fieldType;
		}
	}
}