using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JournalEntry
{
	public int Round { get; private set; }
	public MissionInfo Mission { get; private set; }
	public JournalEntryType EntryType { get; private set; }

	public class Factory: Factory<JournalEntry>
	{
		public JournalEntry Create(int round, MissionInfo mission, JournalEntryType entryType)
		{
			JournalEntry result = Create();
			result.Round = round;
			result.Mission = mission;
			result.EntryType = entryType;
			return result;
		}
	}
}
