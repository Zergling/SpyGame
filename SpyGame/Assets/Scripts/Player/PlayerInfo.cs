using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInfo 
{
	public int Id { get; private set; }
	public Color Background { get; private set; }
	public Dictionary<Region, List<AgentInfo>> AgentsDict { get; private set; }
	public List<AgentInfo> AgentsList { get; private set; }
	public List<JournalEntry> Journal { get; private set; }
	public List<MissionInfo> Missions { get; private set; }

	private GameConfig _gameConfig;

	public PlayerInfo(GameConfig gameConfig)
	{
		_gameConfig = gameConfig;
	}

	private void UpdateAgentList()
	{
		AgentsList.Clear();
		var regions = _gameConfig.regionsCount;
		for (int i = 0; i < regions; i++) 
		{
			Region reg = (Region)i;
			var list = AgentsDict[reg];
			for (int j = 0; j < list.Count; j++)
				AgentsList.Add(list[j]);
		}
	}

	public void SwapAgents(AgentInfo one, AgentInfo two)
	{
		AgentsDict[one.Region].RemoveAt(one.Rank);
		AgentsDict[one.Region].Insert(one.Rank, two);

		AgentsDict[two.Region].RemoveAt(two.Rank);
		AgentsDict[two.Region].Insert(two.Rank, one);

		int oneRank = one.Rank;
		Region oneRegion = one.Region;

		one.UpdateInfo(two.Rank, two.Region);
		two.UpdateInfo(oneRank, oneRegion);
		UpdateAgentList();
	}

	public List<AgentInfo> GetNonSpyAgents(int rank)
	{
		List<AgentInfo> result = new List<AgentInfo>();

		var spawnRegions = _gameConfig.SpySpawnRegions;
		for (int i = 0; i < spawnRegions.Count; i++) 
		{
			Region region = (Region)i;
			AgentInfo agent = AgentsDict[region][rank];
			if (agent.SpyOwner == -1)
				result.Add(agent);
		}

		return result;
	}

	public void AddJournalEntry(JournalEntry entry)
	{
		Journal.Insert(0, entry);
	}

	public void AddMission(MissionInfo mission)
	{
		Missions.Insert(0, mission);
	}

	public class Factory: Factory<PlayerInfo> 
	{
		[Inject] private AgentInfo.Factory _agentFactory;
		[Inject] private SpriteManager _spriteManager;
		[Inject] private GameConfig _gameConfig;

		public PlayerInfo Create(int id)
		{
			PlayerInfo result = Create();
			result.Id = id;
			result.AgentsDict = new Dictionary<Region, List<AgentInfo>>();
			for (int i = 0; i < _gameConfig.regionsCount; i++) 
			{
				Region region = (Region)i;
				result.AgentsDict.Add(region, new List<AgentInfo>());
				for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
				{
					Sprite portrait = _spriteManager.GetRandomPortrait();
					AgentInfo agent = _agentFactory.Create(portrait, id, j, region);
					result.AgentsDict[region].Add(agent);
				}
			}

			result.AgentsList = new List<AgentInfo>();
			result.UpdateAgentList();

			result.Journal = new List<JournalEntry>();
			return result;
		}
	}
}
