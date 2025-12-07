using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AutoAssigned : PropertyAttribute
	{
		public readonly AssignMode AssignMode;
		public readonly Type FieldType;

		public AutoAssigned(AssignMode assignMode, Type fieldType)
		{
			AssignMode = assignMode;
			FieldType = fieldType;
		}
	}
}