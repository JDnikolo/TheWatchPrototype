using Agents;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(AgentInputHandler))]
	public sealed class AgentInputHandlerEditor : EditorBase
	{
		public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

		protected override void OnInspectorGUIInternal()
		{
			if (!EditorApplication.isPlaying) return;
			var local = (AgentInputHandler) target;
			local.MovementBehavior.Display("Movement Behaviour");
		}
	}
}