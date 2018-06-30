using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AgentInfo
{
	public Sprite Portrait { get; private set; }
	public int PlayerOwner { get; private set; }
	public int SpyOwner { get; private set; }
	public int Rank { get; private set; }
	public Region Region { get; private set; }

	public void SetSpy(int spyOwner)
	{
		SpyOwner = spyOwner;
	}

	public void UpdateInfo(int rank, Region region)
	{
		Rank = rank;
		Region = region;
	}

	public class Factory: Factory<AgentInfo>
	{
		public AgentInfo Create(Sprite portrait, int playerOwner, int rank, Region region)
		{
			AgentInfo result = Create();
			result.Portrait = portrait;
			result.PlayerOwner = playerOwner;
			result.SpyOwner = -1;
			result.Rank = rank;
			result.Region = region;
			return result;
		}

		public AgentInfo Create(AgentInfo agent)
		{
			AgentInfo result = Create();
			result.Portrait = agent.Portrait;
			result.PlayerOwner = agent.PlayerOwner;
			result.SpyOwner = agent.SpyOwner;
			result.Rank = agent.Rank;
			result.Region = agent.Region;
			return result;
		}
	}
}
