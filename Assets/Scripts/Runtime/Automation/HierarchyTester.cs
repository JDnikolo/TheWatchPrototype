using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities;
#if UNITY_EDITOR
namespace Runtime.Automation
{
	public struct HierarchyTester<T> where T : MonoBehaviour
	{
		private List<T> m_tempList;
		private bool m_newElements;

		public bool PerformTest(MonoBehaviour instance, ref T[] behaviours)
		{
			m_tempList = new List<T>();
			m_newElements = false;
			var parent = instance.transform;
			var childCount = parent.childCount;
			for (var i = 0; i < childCount; i++)
			{
				if (!parent.GetChild(i).TryGetComponent(out T monoBehaviour)) continue;
				var index = m_tempList.Count;
				if (behaviours == null || !behaviours.TryGetValue(index, out var oldBehavior) ||
					oldBehavior != monoBehaviour) m_newElements = true;
				m_tempList.Add(monoBehaviour);
			}

			if (!m_newElements && behaviours.SafeCount() == m_tempList.SafeCount()) return false;
			var array = new T[m_tempList.Count];
			for (var i = m_tempList.Count - 1; i >= 0; i--)
			{
				array[i] = m_tempList[i];
				m_tempList.RemoveAt(i);
			}
			
			behaviours = array;
			EditorUtility.SetDirty(instance);
			return true;
		}
	}
}
#endif