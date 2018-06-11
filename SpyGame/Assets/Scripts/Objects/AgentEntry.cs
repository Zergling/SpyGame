using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEntry
{
	public Sprite Portrait { get; private set; }
	public int PlayerOwner { get; private set; }
	public AgentTag Tag { get; private set; }
	public AgentRank Rank { get; private set; }
	public WorkRegion Region { get; private set; }

	public AgentEntry(Sprite portrait, int playerOwner, AgentTag tag, AgentRank rank, WorkRegion region)
	{
		Portrait = portrait;
		PlayerOwner = playerOwner;
		Tag = tag;
		Rank = rank;
		Region = region;
	}
}
