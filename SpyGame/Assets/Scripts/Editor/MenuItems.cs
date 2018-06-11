using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using SimpleJson;

public static class MenuItems
{
#region Android
	[MenuItem("Tools/Build/Android/Test", false, 1)]
	public static void BuildAndroidDev()
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) 
		{
			Debug.LogError("SWITCH TO ANDROID");
			return;
		}
		BuildAndroid(BuildType.Test);
	}

	[MenuItem("Tools/Build/Android/Dev", false, 2)]
	public static void BuildAndroiShow()
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) 
		{
			Debug.LogError("SWITCH TO ANDROID");
			return;
		}
		BuildAndroid(BuildType.Dev);
	}

	[MenuItem("Tools/Build/Android/Release", false, 3)]
	public static void BuildAndroiRelease()
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) 
		{
			Debug.LogError("SWITCH TO ANDROID");
			return;
		}
		BuildAndroid(BuildType.Release);
	}

	private static void BuildAndroid(BuildType buildType)
	{
		string keystorePasswordPath = Path.Combine(Application.dataPath, "../../Android/keystore.txt");
		if (!File.Exists(keystorePasswordPath)) 
		{
			Debug.LogError("KEYSTORE PASSOWRD FILE DOES NOT EXIST!!!");
			return;
		}

		string str = File.ReadAllText(keystorePasswordPath);
		JsonObject json = SimpleJson.SimpleJson.DeserializeObject<JsonObject>(str);
		string keystorePath = Path.Combine(Application.dataPath, "../../Android/keystore-jdk8.keystore");
		BuildConfig buildConfig = AssetDatabase.LoadAssetAtPath<BuildConfig>("Assets/Configs/BuildConfig.asset");

		string[] scenes = new string[EditorBuildSettings.scenes.Length];
		for (int i = 0; i < scenes.Length; i++)
			scenes[i] = EditorBuildSettings.scenes[i].path;

		string newVersion = string.Empty;
		string[] versionSplit = buildConfig.android.version.Split('.'); // 2 - test; 1 - dev; 0 - release;
		int[] versionInt = new int[versionSplit.Length];
		for (int i = 0; i < versionSplit.Length; i++)
			versionInt[i] = int.Parse(versionSplit[i]);

		int versionCode = int.Parse(buildConfig.android.versionCode);

		switch (buildType) 
		{
			case BuildType.Test:
				versionInt[2]++;
				break;

			case BuildType.Dev:
				versionInt[1]++;
				versionInt[2] = 0;
				break;

			case BuildType.Release:
				versionInt[0]++;
				versionInt[1] = 0;
				versionInt[2] = 0;
				break;
		}

		versionCode++;
		newVersion = string.Format("{0}.{1}.{2}", versionInt[0], versionInt[1], versionInt[2]);

		buildConfig.android.version = newVersion;
		buildConfig.android.versionCode = versionCode.ToString();
		buildConfig.SetDirty();

		BuildPlayerOptions buildOptions = new BuildPlayerOptions();
		buildOptions.scenes = scenes;
		buildOptions.locationPathName = string.Format("../Builds/Android/build-{0}-({1}).apk", newVersion, versionCode);
		buildOptions.target = BuildTarget.Android;
		buildOptions.options = BuildOptions.None;

		PlayerSettings.applicationIdentifier = buildConfig.android.packageName;
		PlayerSettings.bundleVersion = newVersion;
		PlayerSettings.Android.bundleVersionCode = versionCode;

		PlayerSettings.Android.keystoreName = keystorePath;
		PlayerSettings.Android.keystorePass = json.GetString("keystore-pass");
		PlayerSettings.Android.keyaliasName = json.GetString("keyalias-name");
		PlayerSettings.Android.keyaliasPass = json.GetString("keyalias-pass");

		BuildPipeline.BuildPlayer(buildOptions);
		Debug.Log("ANDROID BUILD SUCCESS");
	}
#endregion

#region iOS
#endregion

#region PC
#endregion

#region Main
#endregion

	public enum BuildType
	{
		Test = 0,
		Dev = 1,
		Release = 2,
	}
}