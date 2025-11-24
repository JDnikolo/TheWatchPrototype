using UI.Text;
using UnityEditor;
using Label = UI.Elements.Label;

namespace Editor
{
	[CustomEditor(typeof(Label))]
	public sealed class LabelEditor : UnityEditor.Editor
	{
		private SerializedProperty m_textWriter;

		private void OnEnable() => m_textWriter = serializedObject.FindProperty("textWriter");

		private void OnDisable() => m_textWriter = null;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (m_textWriter.objectReferenceValue is ITextMeshProvider provider)
			{
				var textMesh = provider.TextMesh;
				if (textMesh) CreateEditor(textMesh).OnInspectorGUI();
			}
		}
	}
}