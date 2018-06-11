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
	}
}
