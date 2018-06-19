using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Agent
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

	public class Factory: Factory<Agent>
	{
		public Agent Create(Sprite portrait, int playerOwner, int rank, Region region)
		{
			Agent result = Create();
			result.Portrait = portrait;
			result.PlayerOwner = playerOwner;
			result.SpyOwner = -1;
			result.Rank = rank;
			result.Region = region;
			return result;
		}

		public Agent Create(Agent agent)
		{
			Agent result = Create();
			result.Portrait = agent.Portrait;
			result.PlayerOwner = agent.PlayerOwner;
			result.SpyOwner = agent.SpyOwner;
			result.Rank = agent.Rank;
			result.Region = agent.Region;
			return result;
		}
	}
}
