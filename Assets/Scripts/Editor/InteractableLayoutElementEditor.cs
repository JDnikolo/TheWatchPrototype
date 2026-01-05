using Interactables.Actions.Layout;
using UI.Layout;
using UI.Layout.Elements;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(InteractableLayoutElement))]
	[CanEditMultipleObjects]
	public sealed class InteractableLayoutElementEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			var local = (InteractableLayoutElement) target;
			if (local.Target && GUILayout.Button("Double Connect"))
			{
				var other = local.Target.GetComponent<InteractableLayoutElement>();
				if (other) other.Target = local.GetComponent<LayoutElementBase>();
			}
		}
	}
}