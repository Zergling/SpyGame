using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SabotageInfo
{
	public PlayerInfo Player { get; private set; }
	public JournalEntry Entry { get; private set; }

	public class Factory: Factory<SabotageInfo> 
	{
		public SabotageInfo Create(PlayerInfo player, JournalEntry entry)
		{
			SabotageInfo result = Create();
			result.Player = player;
			result.Entry = entry;
			return result;
		}
	}
}
