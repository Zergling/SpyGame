using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerAgentsWindow : UpdatableWindowBase
{
	[SerializeField] private Text _title;
	[SerializeField] private Transform _agentsItemsContainer;
	[SerializeField] private List<List<AgentItemUI>> _agentsTable;
	[SerializeField] private List<AgentItemUI> _agentsList;

	private bool _isExchangeMode;
	private Player _player;

	private PlayerAgentItemUI _first;
	private PlayerAgentItemUI _second;

	[Inject] private GameConfig _gameConfig;

	public Transform AgentsItemsContainer { get { return _agentsItemsContainer; } }
	public List<AgentItemUI> AgentsList { get { return _agentsList; } }

	protected override void OnInit()
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

	protected override void OnUpdateInfo(object info)
	{
		_player = (Player)info;

		var agents = _player.Agents;
		for (int i = 0; i < agents.Count; i++) 
		{
			Region region = (Region)i;
			var list = agents[region];
			for (int j = 0; j < list.Count; j++) 
			{
				var agent = list[j];
				_agentsTable[i][j].UpdateInfo(agent);
			}
		}
	}

	protected override void OnShowStart()
	{
		_first = null;
		_second = null;

		_isExchangeMode = false;
		for (int i = 0; i < _agentsList.Count; i++)
			_agentsList[i].interactable = false;

		_title.text = "My agents";
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerWindow>(_player);
	}

	public void OnSwapButtonClick()
	{
		_isExchangeMode = !_isExchangeMode;
		for (int i = 0; i < _agentsList.Count; i++)
			_agentsList[i].interactable = _isExchangeMode;

		_title.text = _isExchangeMode ? "Spaw mode" : "My agents";
	}

	public void OnItemClicked(PlayerAgentItemUI item)
	{
		if (_first == null)
			_first = item;
		else if (_second == null) 
		{
			_second = item;
			_player.SwapAgents(_first.Info, _second.Info);
			_first.Reset();
			_second.Reset();
			_first = null;
			_second = null;
			OnSwapButtonClick();
			OnUpdateInfo(_player);
		}
	}
}
