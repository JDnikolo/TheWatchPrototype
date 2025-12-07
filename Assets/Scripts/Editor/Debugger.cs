using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Debugging;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using Asset = BaseObject;
using Behaviour = BaseBehaviour;

namespace Editor
{
	public static class Debugger
	{
		[MenuItem("Debugging/Debug Scene Behaviours")]
		public static void DebugSceneBehaviours()
		{
			Debug.Log("Debug Scene Behaviours Begin");
			var globalObjects = new HashSet<DebugObject>();
			var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			for (var i = 0; i < rootObjects.Length; i++)
				IterateTransform(rootObjects[i].transform, globalObjects, new List<Behaviour>(), string.Empty);
			RunDebug(globalObjects);
			Debug.Log("Debug Scene Behaviours End");
		}

		private static void IterateTransform(Transform transform, HashSet<DebugObject> globalObjects,
			List<Behaviour> localBehaviours, string path)
		{
			path = $"{path}{transform.gameObject.name}";
			transform.GetComponents(localBehaviours);
			for (var i = localBehaviours.Count - 1; i >= 0; i--)
			{
				var localBehaviour = localBehaviours[i];
				globalObjects.Add(new DebugObject(localBehaviour, 
					transform.gameObject, $"{path}/{localBehaviour.GetType()}"));
			}
			var childCount = transform.childCount;
			for (var i = 0; i < childCount; i++)
				IterateTransform(transform.GetChild(i), globalObjects, localBehaviours, $"{path}/");
		}

		[MenuItem("Debugging/Debug Prefab Behaviours")]
		public static void DebugPrefabBehaviours()
		{
			Debug.Log("Debug Prefab Behaviours Begin");
			var globalObjects = new HashSet<DebugObject>();
			var dataPath = Application.dataPath;
			var prefix = dataPath.Substring(0, dataPath.Length - 6);
			IterateDirectory(new DirectoryInfo($"{dataPath}/Prefabs"),
				globalObjects, new List<Behaviour>(), prefix);
			RunDebug(globalObjects);
			Debug.Log("Debug Prefab Behaviours End");
		}

		[MenuItem("Debugging/Debug Scriptable Objects")]
		public static void DebugScriptableObjects()
		{
			Debug.Log("Debug Scriptable Objects Begin");
			var globalObjects = new HashSet<DebugObject>();
			var dataPath = Application.dataPath;
			var prefix = dataPath.Substring(0, dataPath.Length - 6);
			IterateDirectory(new DirectoryInfo(dataPath), globalObjects, null, prefix);
			RunDebug(globalObjects);
			Debug.Log("Debug Scriptable Objects End");
		}

		private static void IterateDirectory(DirectoryInfo directoryInfo, HashSet<DebugObject> globalObjects,
			List<Behaviour> localBehaviours, string prefix)
		{
			var files = directoryInfo.GetFiles();
			for (var i = 0; i < files.Length; i++)
				IterateFile(files[i], globalObjects, localBehaviours, prefix);
			var directories = directoryInfo.GetDirectories();
			for (var i = 0; i < directories.Length; i++)
				IterateDirectory(directories[i], globalObjects, localBehaviours, prefix);
		}

		private static void IterateFile(FileInfo fileInfo, HashSet<DebugObject> globalObjects,
			List<Behaviour> localBehaviours, string prefix)
		{
			var path = fileInfo.FullName;
			if (localBehaviours == null)
			{
				if (!path.EndsWith(".asset")) return;
				path = path.RemovePrefix(prefix);
				if (AssetDatabase.LoadMainAssetAtPath(path) is Asset scriptableObject)
					globalObjects.Add(new DebugObject(scriptableObject, 
						scriptableObject, path.Replace('\\', '/').RemovePostfix(".asset")));
			}
			else
			{
				if (!path.EndsWith(".prefab")) return;
				path = path.RemovePrefix(prefix);
				if (AssetDatabase.LoadMainAssetAtPath(path) is GameObject gameObject)
					IterateTransform(gameObject.transform, globalObjects, localBehaviours,
						path[..(path.LastIndexOfAny(new[] {'\\', '/'}) + 1)].Replace('\\', '/'));
			}
		}

		private static void RunDebug(HashSet<DebugObject> globalObjects)
		{
			Parallel.ForEach(globalObjects, IterateObject);
			GC.Collect();
		}

		private static void IterateObject(DebugObject obj) =>
			new OperationData(obj.TestInstance, true).TestAny(obj.DebugString, obj.Instance);
	}
}