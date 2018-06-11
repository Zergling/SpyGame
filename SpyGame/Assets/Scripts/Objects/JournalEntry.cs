using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JournalEntry
{
	public int PlayerId { get; private set; }
	public int Region { get; private set; }

	public class Factory: Factory<JournalEntry>
	{
		public JournalEntry Create(int id, int region)
		{
			JournalEntry result = Create();
			result.PlayerId = id;
			result.Region = region;
			return result;
		}
	}
}
