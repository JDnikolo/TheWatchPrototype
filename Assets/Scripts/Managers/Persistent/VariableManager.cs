using Attributes;
using Runtime;
using UnityEditor;
using UnityEngine;
using Variables;

namespace Managers.Persistent
{
	public sealed class VariableManager : Singleton<VariableManager>
	{
		[SerializeField, DisableInInspector] private VariableObject[] variables;
		
		protected override bool Override => false;

		public void ResetVariables()
		{
			for (var i = 0; i < variables.Length; i++) variables[i].ResetValue();
		}
#if UNITY_EDITOR
		public void SetVariables(VariableObject[] newVariables)
		{
			variables = newVariables;
			EditorUtility.SetDirty(this);
		}
#endif
	}
}