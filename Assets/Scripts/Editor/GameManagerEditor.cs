using System;
using Collections;
using Managers.Persistent;
using Runtime.FixedUpdate;
using Runtime.FrameUpdate;
using Runtime.LateUpdate;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying)
			{
				ApplyModifications();
				return;
			}
		
			var local = (GameManager) target;
			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.LabelField("Frame Update");
				EditorGUILayout.EnumPopup("Invocation Level", local.FrameUpdateInvoke);
				DisplayCollection(local.FrameUpdateCollection, FrameInverter);
				EditorGUILayout.LabelField("Late Update");
				EditorGUILayout.EnumPopup("Invocation Level", local.LateUpdateInvoke);
				DisplayCollection(local.LateUpdateCollection, LateInverter);
				EditorGUILayout.LabelField("Fixed Update");
				EditorGUILayout.EnumPopup("Invocation Level", local.FixedUpdateInvoke);
				DisplayCollection(local.FixedUpdateCollection, FixedInverter);
			}
		}
		
		private FrameUpdatePosition FrameInverter(byte value) => (FrameUpdatePosition) value;
		
		private LateUpdatePosition LateInverter(byte value) => (LateUpdatePosition) value;
		
		private FixedUpdatePosition FixedInverter(byte value) => (FixedUpdatePosition) value;

		private void DisplayCollection<T, TEnum>(OrderedCollection<T> collection, Func<byte, TEnum> inverter)
		{
			if (collection == null) return;
			var innerCollection = collection.Collections;
			if (innerCollection == null || innerCollection.Count == 0) return;
			EditorGUI.indentLevel += 1;
			foreach (var pair in innerCollection)
			{
				EditorGUILayout.LabelField($"Position: {inverter(pair.Key)}");
				var valueSet = pair.Value;
				if (valueSet == null || valueSet.Count == 0) continue;
				EditorGUI.indentLevel += 1;
				foreach (var value in valueSet) value.Display(null);
				EditorGUI.indentLevel -= 1;
			}

			EditorGUI.indentLevel -= 1;
		}
	}
}