using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AgentsPage))]
public class AgentsPageEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE AGENTS LIST"))
			UpdateAgentsList();
	}

	private void UpdateAgentsList()
	{
		AgentsPage page = (AgentsPage)target;
		var list = page.AgentsList;
		list.Clear();
		list.AddRange(page.AgentsContainer.GetComponentsInChildren<AgentItemUI>(true));
	}
}
