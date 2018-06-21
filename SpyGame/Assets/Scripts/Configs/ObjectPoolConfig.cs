using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolConfigInternal;

namespace ObjectPoolConfigInternal
{
	[Serializable]
	public class PoolObject
	{
		public GameObject poolObject;
		public int count;
	}
}

public class ObjectPoolConfig : ScriptableObject
{
	public PoolObject journalPage;
}
