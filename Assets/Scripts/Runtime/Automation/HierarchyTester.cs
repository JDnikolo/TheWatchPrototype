using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities;
#if UNITY_EDITOR
namespace Runtime.Automation
{
	public struct HierarchyTester<T> where T : MonoBehaviour
	{
		private bool m_newElements;

		public bool PerformTest(MonoBehaviour instance, ref T[] behaviours, 
			Transform parent = null, Action<T> onRemoved = null, Action<T> onAssigned = null)
		{
			var tempList = new List<T>();
			m_newElements = false;
			if (!parent) parent = instance.transform;
			var childCount = parent.childCount;
			for (var i = 0; i < childCount; i++)
			{
				if (!parent.GetChild(i).TryGetComponent(out T monoBehaviour)) continue;
				var index = tempList.Count;
				if (behaviours == null || !behaviours.TryGetValue(index, out var oldBehavior)) 
					m_newElements = true;
				else if (oldBehavior != monoBehaviour)
				{
					m_newElements = true;
					onRemoved?.Invoke(oldBehavior);
				}

				tempList.Add(monoBehaviour);
			}

			if (!m_newElements && behaviours.SafeCount() == tempList.SafeCount()) return false;
			var array = new T[tempList.Count];
			for (var i = tempList.Count - 1; i >= 0; i--)
			{
				onAssigned?.Invoke(array[i] = tempList[i]);
				tempList.RemoveAt(i);
			}
			
			behaviours = array;
			EditorUtility.SetDirty(instance);
			return true;
		}
	}
}
#endif