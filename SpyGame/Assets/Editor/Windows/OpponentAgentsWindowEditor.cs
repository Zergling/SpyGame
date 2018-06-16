using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OpponentAgentsWindow))]
public class OpponentAgentsWindowEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE PAGES LIST"))
			UpdatePagesList();
	}

	private void UpdatePagesList()
	{
		OpponentAgentsWindow window = (OpponentAgentsWindow)target;
		var pages = window.AgentsPages;
		pages.Clear();
		pages.AddRange(window.PagesContainer.GetComponentsInChildren<AgentsPage>(true));
	}
}
