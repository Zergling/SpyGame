using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OperationsWindow))]
public class OperationsWindowContainer : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE AGENTS LIST"))
			UpdateAgents();

		if (GUILayout.Button("UPDATE REGION SELECTORS LIST"))
			UpdateRegionSelectors();
	}

	private void UpdateAgents()
	{
		OperationsWindow window = (OperationsWindow)target;
		var list = window.Agents;
		list.Clear();
		list.AddRange(window.AgentsContainer.GetComponentsInChildren<AgentItemUI>(true));
	}

	private void UpdateRegionSelectors()
	{
		OperationsWindow window = (OperationsWindow)target;
		var list = window.RegionSelectors;
		list.Clear();
		list.AddRange(window.RegionSelectorsContainer.GetComponentsInChildren<RegionSelector>(true));
	}
}
