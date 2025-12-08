using UI.Elements;
using UnityEditor;
using Utilities;

namespace Editor
{
	[CustomEditor(typeof(Slider))]
	[CanEditMultipleObjects]
	public sealed class SliderEditor : ElementBaseEditor
	{
		private SerializedProperty m_wholeNumbers;
		private SerializedProperty m_lowerValue;
		private SerializedProperty m_upperValue;
		private SerializedProperty m_lowerValueInt;
		private SerializedProperty m_upperValueInt;
		private SerializedProperty m_speedMultiplier;
		
		protected override void OnEnable()
		{
			base.OnEnable();
			m_wholeNumbers = serializedObject.FindProperty("wholeNumbers");
			m_lowerValue = serializedObject.FindProperty("lowerValue");
			m_upperValue = serializedObject.FindProperty("upperValue");
			m_lowerValueInt = serializedObject.FindProperty("lowerValueInt");
			m_upperValueInt = serializedObject.FindProperty("upperValueInt");
			m_speedMultiplier = serializedObject.FindProperty("speedMultiplier");
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_wholeNumbers = null;
			m_lowerValue = null;
			m_upperValue = null;
			m_lowerValueInt = null;
			m_upperValueInt = null;
			m_speedMultiplier = null;
		}

		protected override void DisplayBeforeHidden()
		{
			base.DisplayBeforeHidden();
			EditorGUILayout.PropertyField(m_wholeNumbers);
			if (!m_wholeNumbers.boolValue)
			{
				EditorGUILayout.PropertyField(m_lowerValue);
				EditorGUILayout.PropertyField(m_upperValue);
				EditorGUILayout.PropertyField(m_speedMultiplier);
			}
			else
			{
				EditorGUILayout.PropertyField(m_lowerValueInt);
				EditorGUILayout.PropertyField(m_upperValueInt);
			}

			ApplyModifications();
		}
		
		protected override void DisplayHidden()
		{
			base.DisplayHidden();
			var local = (Slider) target;
			if (EditorApplication.isPlaying)
			{
				EditorGUILayout.Toggle("Selected", local.Selected);
				if (m_wholeNumbers.boolValue) local.IntReceiver.Display("Callback Target");
				else local.FloatReceiver.Display("Callback Target");
			}
		}
	}
}