using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Agent
{
	public Sprite Portrait { get; private set; }
	public int PlayerOwner { get; private set; }
	public AgentTag Tag { get; private set; }
	public int Rank { get; private set; }
	public int Region { get; private set; }

	public void SetTag(AgentTag tag)
	{
		Tag = tag;
	}

	public void UpdateRankAndRegion(int rank, int region)
	{
		Rank = rank;
		Region = region;
	}

	public class Factory: Factory<Agent>
	{
		public Agent Create(Sprite portrait, int playerOwner, AgentTag tag, int rank, int region)
		{
			Agent result = Create();
			result.Portrait = portrait;
			result.PlayerOwner = playerOwner;
			result.Tag = tag;
			result.Rank = rank;
			result.Region = region;
			return result;
		}

		public Agent Create(Agent agent)
		{
			Agent result = Create();
			result.Portrait = agent.Portrait;
			result.PlayerOwner = agent.PlayerOwner;
			result.Tag = agent.Tag;
			result.Rank = agent.Rank;
			result.Region = agent.Region;
			return result;
		}
	}
}
