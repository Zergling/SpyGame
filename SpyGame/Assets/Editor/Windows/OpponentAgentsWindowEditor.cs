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

		if (GUILayout.Button("UPDATE LISTS"))
			UpdateLists();
	}

	private void UpdateLists()
	{
		OpponentAgentsWindow window = (OpponentAgentsWindow)target;

		var agentItems = window.AgentItems;
		var selectors = window.OpponentSelectors;

		agentItems.Clear();
		agentItems.AddRange(window.GetComponentsInChildren<AgentItemUI>(true));

		selectors.Clear();
		selectors.AddRange(window.GetComponentsInChildren<OpponentSelectButton>(true));
	}
}
