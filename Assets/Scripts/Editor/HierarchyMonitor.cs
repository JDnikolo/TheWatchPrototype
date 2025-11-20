using System;
using System.Collections.Generic;
using Runtime.Automation;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
	[InitializeOnLoad]
	public static class HierarchyMonitor
	{
		private static readonly List<MonoBehaviour> m_behaviours = new();
		private static readonly List<IBehaviourChecker> m_behaviourChecker = new();
		private static DateTime m_lastCheckTime;
		
		static HierarchyMonitor()
		{
			m_lastCheckTime = DateTime.MinValue;
			EditorApplication.hierarchyChanged += OnHierarchyChanged;
		}

		private static void OnHierarchyChanged()
		{
			var now = DateTime.Now;
			if (now - m_lastCheckTime < TimeSpan.FromSeconds(1)) return;
			m_lastCheckTime = now;
			m_behaviours.GetAllBehavioursInScene();
			for (var i = m_behaviours.Count - 1; i >= 0; i--)
			{
				var behavior = m_behaviours[i];
				if (behavior is IHierarchyChanged hierarchyChanged) hierarchyChanged.OnHierarchyChanged();
				if (behavior is IBehaviourChecker behaviorChecker)
				{
					m_behaviourChecker.Add(behaviorChecker);
					behaviorChecker.OnCheckBehaviourStart();
				}

				//Behaviors with this interface are ignored
				if (behavior is IAutomationIgnore) m_behaviours.RemoveAtFast(i);
			}

			for (var i = m_behaviours.Count - 1; i >= 0; i--)
			{
				for (var j = 0; j < m_behaviourChecker.Count; j++) 
					m_behaviourChecker[j].OnCheckBehaviour(m_behaviours[i]);
				m_behaviours.RemoveAt(i);
			}

			for (var i = m_behaviourChecker.Count - 1; i >= 0; i--)
			{
				m_behaviourChecker[i].OnCheckBehaviourEnd();
				m_behaviourChecker.RemoveAt(i);
			}
		}
	}
}