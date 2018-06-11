using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildConfigInternal;

namespace BuildConfigInternal
{
	[Serializable]
	public class AndroidSettings
	{
		public string packageName;
		public string version;
		public string versionCode;
	}

	[Serializable]
	public class iOSSettings
	{
		public string bundle;
		public string version;
		public string versionCode;
	}

	[Serializable]
	public class PCSettings
	{
		public string bundleIdentifier;
		public string version;
		public string buildNubmer;
	}
}

public class BuildConfig : ScriptableObject
{
	public AndroidSettings android;
	public iOSSettings iOS;
	public PCSettings pc;
}
