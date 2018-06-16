using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAgentsWindow))]
public class PlayerAgentsWindowEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE AGENTS LIST"))
			UpdateAgentsList();
	}

	private void UpdateAgentsList()
	{
		PlayerAgentsWindow window = (PlayerAgentsWindow)target;
		var list = window.AgentsList;
		list.Clear();

		list.AddRange(window.AgentsItemsContainer.GetComponentsInChildren<AgentItemUI>(true));
	}
}
