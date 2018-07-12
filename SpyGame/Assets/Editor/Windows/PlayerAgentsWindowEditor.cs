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

		if (GUILayout.Button("UPDATE LIST"))
			UpdateAgentItemsList();
	}

	private void UpdateAgentItemsList()
	{
		PlayerAgentsWindow window = (PlayerAgentsWindow)target;
		var list = window.AgentItems;
		list.Clear();
		AgentItemUI[] items = window.GetComponentsInChildren<AgentItemUI>(true);
		list.AddRange(items);
	}
}
