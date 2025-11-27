using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities;
#if UNITY_EDITOR
namespace Runtime.Automation
{
	public struct BehaviorTester
	{
		private List<MonoBehaviour> m_tempList;
		private bool m_newElements;

		public void BeginTest()
		{
			m_tempList = new List<MonoBehaviour>();
			m_newElements = false;
		}

		public bool TestBehavior<T>(MonoBehaviour monoBehaviour, ref MonoBehaviour[] behaviours)
		{
			var result = false;
			if (m_tempList != null && monoBehaviour is T)
			{
				var index = m_tempList.Count;
				if (behaviours == null || !behaviours.ToIList().TryGetValue(index, out var oldBehavior) ||
					oldBehavior != monoBehaviour)
				{
					result = true;
					m_newElements = true;
				}
				
				m_tempList.Add(monoBehaviour);
			}

			return result;
		}

		public bool EndTest(MonoBehaviour instance, ref MonoBehaviour[] behaviours)
		{
			if (!m_newElements && behaviours.SafeCount() == m_tempList.SafeCount()) return false;
			var array = new MonoBehaviour[m_tempList.Count];
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