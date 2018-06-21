using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player 
{
	public int Id { get; private set; }
	public Sprite Background { get; private set; }
	public Dictionary<Region, List<Agent>> Agents { get; private set; }
	public List<JournalEntry> Journal { get; private set; }

	private GameConfig _gameConfig;

	public Player(GameConfig gameConfig)
	{
		_gameConfig = gameConfig;
	}

	public void SwapAgents(Agent one, Agent two)
	{
		Agents[one.Region].RemoveAt(one.Rank);
		Agents[one.Region].Insert(one.Rank, two);

		Agents[two.Region].RemoveAt(two.Rank);
		Agents[two.Region].Insert(two.Rank, one);

		int oneRank = one.Rank;
		Region oneRegion = one.Region;

		one.UpdateInfo(two.Rank, two.Region);
		two.UpdateInfo(oneRank, oneRegion);
	}

	public List<Agent> GetNonSpyAgents(int rank)
	{
		List<Agent> result = new List<Agent>();

		var spawnRegions = _gameConfig.SpySpawnRegions;
		for (int i = 0; i < spawnRegions.Count; i++) 
		{
			Region region = (Region)i;
			Agent agent = Agents[region][rank];
			if (agent.SpyOwner == -1)
				result.Add(agent);
		}

		return result;
	}

	public void AddJournalEntry(JournalEntry entry)
	{
		Journal.Insert(0, entry);
	}

	public class Factory: Factory<Player> 
	{
		[Inject] private Agent.Factory _agentFactory;
		[Inject] private SpriteConfig _spriteConfig;
		[Inject] private GameConfig _gameConfig;

		public Player Create(int id)
		{
			Player result = Create();
			result.Id = id;
			result.Background = _spriteConfig.GetBackground();
			result.Agents = new Dictionary<Region, List<Agent>>();
			for (int i = 0; i < _gameConfig.regionsCount; i++) 
			{
				Region region = (Region)i;
				result.Agents.Add(region, new List<Agent>());
				for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
				{
					Sprite portrait = _spriteConfig.GetPortrait();
					Agent agent = _agentFactory.Create(portrait, id, j, region);
					result.Agents[region].Add(agent);
				}
			}

			result.Journal = new List<JournalEntry>();
			return result;
		}
	}
}
