using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(SceneStart))]
	public class SceneStartEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (target is not SceneStart local) return;
			if (GUILayout.Button("Parse scene"))
			{
				var collection = new HashSet<MonoBehaviour>();
				var gameObjects = new List<GameObject>();
				gameObjects.GetAllObjectsInScene();
				for (var objectIndex = 0; objectIndex < gameObjects.Count; objectIndex++)
				{
					var gameObject = gameObjects[objectIndex];
					if (!gameObject) continue;
					var components = gameObject.GetComponents<MonoBehaviour>();
					for (var componentIndex = 0; componentIndex < components.Length; componentIndex++)
					{
						var component = components[componentIndex];
						if (!component || component is not IStartable) continue;
						collection.Add(component);
					}
				}

				var array = new MonoBehaviour[collection.Count];
				collection.CopyTo(array);
				local.SetArray(array);
				EditorUtility.SetDirty(local);
			}
		}
	}
}