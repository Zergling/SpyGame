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

		if (GUILayout.Button("UPDATE BACKROUNDS"))
			UpdateBackgorunds();
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

	private void UpdateBackgorunds()
	{
		SpriteConfig config = (SpriteConfig)target;
		string[] findPath = new string[1] { "Assets/Sprites/Back" };
		string filterString = "t:Sprite";
		config.ClearBackgrounds();

		string[] assets = AssetDatabase.FindAssets(filterString, findPath);
		for (int i = 0; i < assets.Length; i++) 
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(assets[i]);
			Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
			config.AddBackground(sprite);
		}

		config.SetDirty();
	}
}