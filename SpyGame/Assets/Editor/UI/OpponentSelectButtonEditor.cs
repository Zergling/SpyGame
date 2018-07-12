using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(OpponentSelectButton))]
public class OpponentSelectButtonEditor : ButtonWithSelectorEditor
{
	SerializedProperty _text;

	protected override void OnEnable()
	{
		base.OnEnable();
		_text = serializedObject.FindProperty("_text");
	}


	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		base.OnInspectorGUI();

		EditorGUILayout.PropertyField(_text);
		serializedObject.ApplyModifiedProperties();
	}
}
