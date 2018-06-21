using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JournalEntry
{
	public int JournalOwnerId { get; private set; }
	public int PlayerId { get; private set; }
	public JournalEntryType entryType { get; private set; }
	public Region Region { get; private set; }
	public bool IsSabotaged { get; private set; }

	public void MarkSabotaged()
	{
		IsSabotaged = true;
	}

	public class Factory: Factory<JournalEntry>
	{
		public JournalEntry Create(int journalOwner, int id, JournalEntryType entryType, Region region)
		{
			JournalEntry result = Create();
			result.JournalOwnerId = journalOwner;
			result.PlayerId = id;
			result.entryType = entryType;
			result.Region = region;
			result.IsSabotaged = false;
			return result;
		}
	}
}
