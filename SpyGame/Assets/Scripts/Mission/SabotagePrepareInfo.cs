using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SabotagePrepareInfo
{
	public PlayerInfo Player { get; private set; }
	public JournalEntry Entry { get; private set; }

	public class Factory: Factory<SabotagePrepareInfo> 
	{
		public SabotagePrepareInfo Create(PlayerInfo player, JournalEntry entry)
		{
			SabotagePrepareInfo result = Create();
			result.Player = player;
			result.Entry = entry;
			return result;
		}
	}
}
