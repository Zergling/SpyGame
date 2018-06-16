using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player 
{
	public int Id { get; private set; }
	public List<List<Agent>> Agents { get; private set; }
	public List<JournalEntry> Journal { get; private set; }

	private Agent.Factory _agentFactory;

	public Player(Agent.Factory agentFactopry)
	{
		_agentFactory = agentFactopry;
	}

	public void SwapAgents(Agent one, Agent two)
	{
		Agents[one.Region].RemoveAt(one.Rank);
		Agents[one.Region].Insert(one.Rank, two);

		Agents[two.Region].RemoveAt(two.Rank);
		Agents[two.Region].Insert(two.Rank, one);

		int oneRank = one.Rank;
		int oneRegion = one.Region;

		one.UpdateRankAndRegion(two.Rank, two.Region);
		two.UpdateRankAndRegion(oneRank, oneRegion);
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
			result.Agents = new List<List<Agent>>();
			for (int i = 0; i < _gameConfig.regionsCount; i++) 
			{
				result.Agents.Add(new List<Agent>());
				for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
				{
					Sprite portrait = _spriteConfig.GetPortrait();
					Agent agent = _agentFactory.Create(portrait, id, AgentTag.None, j, i);
					result.Agents[i].Add(agent);
				}
			}

			result.Journal = new List<JournalEntry>();
			return result;
		}
	}
}
