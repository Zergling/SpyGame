using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissionInfo
{
	public int PlayerId { get; private set; } // кто проводит миссию
	public int Round { get; private set; }
	public Region Region { get; private set; }
	public MissionSecurityLevel SecurityLevel { get; private set; }

	public class Factory: Factory<MissionInfo> 
	{
		public MissionInfo Create(int id, int round, Region region, MissionSecurityLevel security)
		{
			MissionInfo info = Create();
			info.PlayerId = id;
			info.Round = round;
			info.Region = region;
			info.SecurityLevel = security;
			return info;
		}
	}
}
