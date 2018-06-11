using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteConfig))]
public class SpriteConfigEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("UPDATE PORTRAITS"))
			UpdatePortraits();
	}

	private void UpdatePortraits()
	{
		SpriteConfig config = (SpriteConfig)target;
		string[] folders = new string[1] { "Assets/Sprites/Portraits" };
		string filterString = "t:Sprite";
		config.ClearPortraits();

		string[] assets = AssetDatabase.FindAssets(filterString, folders);
		for (int i = 0; i < assets.Length; i++) 
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(assets[i]);
			Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
			config.AddPortrait(sprite);
		}

		config.SetDirty();
	}
}