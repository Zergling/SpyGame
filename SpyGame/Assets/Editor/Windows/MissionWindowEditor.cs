using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MissionWindow))]
public class MissionWindowEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE LISTS"))
			UpdateLists();
	}

	private void UpdateLists()
	{
		MissionWindow window = (MissionWindow)target;

		var agentList = window.AgentItems;
		var regionList = window.RegionSelectors;
		var securityList = window.SecuritySelectors;

		agentList.Clear();
		regionList.Clear();
		securityList.Clear();

		agentList.AddRange(window.GetComponentsInChildren<AgentItemUI>(true));
		regionList.AddRange(window.GetComponentsInChildren<RegionSelector>(true));
		securityList.AddRange(window.GetComponentsInChildren<MissionSecuritySelector>(true));
	}
}
