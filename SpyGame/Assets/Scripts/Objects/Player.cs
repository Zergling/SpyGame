using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player 
{
	public int Id { get; private set; }
	public Dictionary<WorkRegion, List<Agent>> Agents { get; private set; }
	public List<JournalEntry> Journal { get; private set; }

	public class Factory: Factory<Player> {}
}
