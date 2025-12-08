using Input;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(InputBase))]
	public abstract class InputBaseEditor : EditorBase
	{
		private SerializedProperty m_actionReference;
		private SerializedProperty m_textToDisplay;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_actionReference = serializedObject.FindProperty("actionReference");
			m_textToDisplay = serializedObject.FindProperty("textToDisplay");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_actionReference = null;
			m_textToDisplay = null;
		}

		private void OnDestroy() => OnDisable();

		protected override void OnInspectorGUIInternal()
		{
			
		}
	}
}