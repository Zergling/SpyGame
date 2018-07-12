using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JournalPage))]
public class JournalPageEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE ITEMS LIST"))
			UpdateList();
	}

	private void UpdateList()
	{
		JournalPage page = (JournalPage)target;
		page.Items.Clear();
		page.Items.AddRange(page.GetComponentsInChildren<JournalItemUI>(true));
	}
}
