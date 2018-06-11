using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Agent
{
	public Sprite Portrait { get; private set; }
	public int PlayerOwner { get; private set; }
	public AgentTag Tag { get; private set; }
	public AgentRank Rank { get; private set; }
	public WorkRegion Region { get; private set; }

	public class Factory: Factory<Agent>
	{
		public Agent Create(Sprite portrait, int playerOwner, AgentTag tag, AgentRank rank, WorkRegion region)
		{
			Agent result = Create();
			result.Portrait = portrait;
			result.PlayerOwner = playerOwner;
			result.Tag = tag;
			result.Rank = rank;
			result.Region = region;
			return result;
		}
	}
}
