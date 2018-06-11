using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAgentsWindow : UpdatableWindowBase
{
	[SerializeField] private Transform _agentsItemsContainer;
	[SerializeField] private List<List<AgentItemUI>> _agents;

	private Player _player;

	[Inject] private GameConfig _gameConfig;

	protected override void OnInit()
	{
		var items = _agentsItemsContainer.GetComponentsInChildren<AgentItemUI>(true);
		int k = 0;
		_agents = new List<List<AgentItemUI>>();
		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			_agents.Add(new List<AgentItemUI>());
			for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
			{
				_agents[i].Add(items[k]);
				k++;
			}
		}
	}

	protected override void OnUpdateInfo(object info)
	{
		_player = (Player)info;

		var agents = _player.Agents;
		for (int i = 0; i < agents.Count; i++) 
		{
			var list = agents[i];
			for (int j = 0; j < list.Count; j++) 
			{
				var agent = list[j];
				_agents[i][j].UpdateInfo(agent);
			}
		}
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerWindow>(_player);
	}
}
