using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player 
{
	public int Id { get; private set; }
	public List<List<Agent>> Agents { get; private set; }
	public List<JournalEntry> Journal { get; private set; }

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
