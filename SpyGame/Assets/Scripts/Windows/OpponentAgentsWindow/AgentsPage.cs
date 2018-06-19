using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AgentsPage : MonoBehaviour
{
	[SerializeField] private Text _title;
	[SerializeField] private Transform _agentsContainer;
	[SerializeField] private List<AgentItemUI> _agentsList;

	private List<List<AgentItemUI>> _agentsTable;

	public Transform AgentsContainer { get { return _agentsContainer; } }
	public List<AgentItemUI> AgentsList { get { return _agentsList; } }

	[Inject] private GameConfig _gameConfig;

	public void Init()
	{
		int k = 0;
		_agentsTable = new List<List<AgentItemUI>>();

		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			_agentsTable.Add(new List<AgentItemUI>());
			for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
			{
				_agentsTable[i].Add(_agentsList[k]);
				k++;
			}
		}
	}

	public void UpdateInfo (Player player, int viewerId)
	{
		_title.text = string.Format("Player {0} agents", player.Id);
		var agents = player.Agents;
		for (int i = 0; i < agents.Count; i++) 
		{
			Region region = (Region)i;
			var list = agents[region];
			for (int j = 0; j < list.Count; j++)
				_agentsTable[i][j].UpdateInfo(list[j], viewerId);
		}
	}
}
