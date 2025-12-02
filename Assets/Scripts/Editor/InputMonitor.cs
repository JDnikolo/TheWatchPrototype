/*
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Editor
{
	[InitializeOnLoad]
	public static class InputMonitor
	{
		static InputMonitor()
		{
			InputSystem.onActionsChange += OnLayoutChange;
		}

		private static void OnLayoutChange() => Debug.Log("OnLayoutChange");
	}
}
*/