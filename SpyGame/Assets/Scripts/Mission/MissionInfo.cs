using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissionInfo
{
	public int PlayerId { get; private set; } // кто проводит миссию
	public Region Region { get; private set; }
	public SecurityLevel Security { get; private set; }

	public class Factory: Factory<MissionInfo> 
	{
		public MissionInfo Create(int id, Region region, SecurityLevel security)
		{
			MissionInfo info = Create();
			info.PlayerId = id;
			info.Region = region;
			info.Security = security;
			return info;
		}
	}
}
