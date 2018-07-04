using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ButtonWithSelector), true)]
public class ButtonWithSelectorEditor : ButtonEditor
{
	SerializedProperty _selectionImage;

	protected override void OnEnable()
	{
		base.OnEnable();
		_selectionImage = serializedObject.FindProperty("_selectionImage");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		base.OnInspectorGUI();

		EditorGUILayout.PropertyField(_selectionImage);
		serializedObject.ApplyModifiedProperties();
	}
}
