using System.Collections.Generic;
using System.IO;
using Managers.Persistent;
using UnityEditor;
using UnityEngine;
using Utilities;
using Variables;

namespace Editor
{
	[CustomEditor(typeof(VariableManager))]
	public sealed class VariableManagerEditor : EditorBase
	{
		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying && GUILayout.Button("Find All"))
			{
				var dataPath = Application.dataPath;
				var prefix = dataPath.Substring(0, dataPath.Length - 6);
				var variables = new List<VariableObject>();
				IterateDirectory(new DirectoryInfo(Path.Combine(dataPath, "Variables")), variables, prefix);
				var local = (VariableManager) target;
				local.SetVariables(variables.ToArray());
			}
		}

		private static void IterateDirectory(DirectoryInfo directoryInfo, List<VariableObject> variables, string prefix)
		{
			var files = directoryInfo.GetFiles();
			for (var i = 0; i < files.Length; i++)
			{
				var path = files[i].FullName;
				if (!path.EndsWith(".asset")) continue;
				path = path.RemovePrefix(prefix);
				if (AssetDatabase.LoadMainAssetAtPath(path) is VariableObject variableObject)
					variables.Add(variableObject);
			}
			
			var directories = directoryInfo.GetDirectories();
			for (var i = 0; i < directories.Length; i++) IterateDirectory(directories[i], variables, prefix);
		}
	}
}