using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OperationsWindow : UpdatableWindowBase
{
	[SerializeField] private List<AgentItemUI> _agents;
	[SerializeField] private List<RegionSelector> _regionSelectors;

	[SerializeField] private Transform _regionSelectorsContainer;
	[SerializeField] private Transform _agentsContainer;

	private Dictionary<Region, List<AgentItemUI>> _agentsTable;
	private Player _player;
	private Region _region;
	private int _level;
	private List<Agent> _agentsInMission;

	[Inject] private GameController _gameController;
	[Inject] private GameConfig _gameConfig;

	public List<AgentItemUI> Agents { get { return _agents; } }
	public List<RegionSelector> RegionSelectors { get { return _regionSelectors; } }
	public Transform RegionSelectorsContainer { get { return _regionSelectorsContainer; } }
	public Transform AgentsContainer { get { return _agentsContainer; } }

	protected override void OnInit()
	{
		_region = Region.SouthAmerica;
		_level = 0;

		int k = 0;
		_agentsTable = new Dictionary<Region, List<AgentItemUI>>();
		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			Region region = (Region)i;
			_agentsTable[region] = new List<AgentItemUI>();
			for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
			{
				_agentsTable[region].Add(_agents[k]);
				k++;
			}
		}

		_agentsInMission = new List<Agent>();
		for (int i = 0; i < _regionSelectors.Count; i++)
			_regionSelectors[i].UpdateInfo(this);
	}

	protected override void OnUpdateInfo(object info)
	{
		_player = (Player)info;
		for (int i = 0; i < _regionSelectors.Count; i++)
			_regionSelectors[i].FillColor(Region.Unknown);

		var agents = _player.Agents;
		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			Region region = (Region)i;
			var list = agents[region];
			for (int j = 0; j < list.Count; j++) 
			{
				var agent = list[j];
				_agentsTable[region][j].UpdateInfo(agent);
			}
		}
	}

	protected override void OnHideEnd()
	{
		_level = 0;
		_region = Region.Unknown;
		_windowsManager.Show<PlayerWindow>(_player);
	}

	public void OnSecretLevelButtonClick(int level)
	{
		_level = level;
		UpdateAgentsInMission(_region, _level);
		UpdateAgentsTable();
	}

	public void OnRegionButtonClick(Region region)
	{
		_region = region;
		UpdateAgentsInMission(_region, _level);
		UpdateAgentsTable();
		for (int i = 0; i < _regionSelectors.Count; i++)
			_regionSelectors[i].FillColor(_region);
	}

	public void OnSubmitButton()
	{
		_gameController.SubmitMission(_player, _agentsInMission, _region);
		_agentsInMission.Clear();
		Hide();
	}

	private void UpdateAgentsTable()
	{
		var agents = _player.Agents;
		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			Region region = (Region)i;
			var list = agents[region];
			for (int j = 0; j < list.Count; j++) 
			{
				var agent = list[j];
				_agentsTable[region][j].UpdateInfo(agent, _agentsInMission.Contains(agent));
			}
		}
	}

	private void UpdateAgentsInMission(Region region, int level)
	{
		_agentsInMission.Clear();
		int regionInt = (int)region;
		Region leftRegion, rightRegion;
		int leftInt, rightInt;

		leftInt = regionInt-1;
		if (leftInt < 0)
			leftInt = _gameConfig.regionsCount-1;

		rightInt = regionInt+1;
		if (rightInt >= _gameConfig.regionsCount)
			rightInt = 0;

		leftRegion = (Region)leftInt;
		rightRegion = (Region)rightInt;
		switch (level) 
		{
			case 0:
				_agentsInMission.Add(_agentsTable[region][0].Info);
				_agentsInMission.Add(_agentsTable[region][1].Info);
				break;

			case 1:
				_agentsInMission.Add(_agentsTable[region][0].Info);
				_agentsInMission.Add(_agentsTable[region][1].Info);
				_agentsInMission.Add(_agentsTable[rightRegion][0].Info);
				_agentsInMission.Add(_agentsTable[leftRegion][0].Info);
				break;

			case 2:
				_agentsInMission.Add(_agentsTable[region][0].Info);
				_agentsInMission.Add(_agentsTable[region][1].Info);
				_agentsInMission.Add(_agentsTable[region][2].Info);
				_agentsInMission.Add(_agentsTable[rightRegion][0].Info);
				_agentsInMission.Add(_agentsTable[rightRegion][1].Info);
				_agentsInMission.Add(_agentsTable[leftRegion][0].Info);
				_agentsInMission.Add(_agentsTable[leftRegion][1].Info);
				break;

			case 3:
				_agentsInMission.Add(_agentsTable[region][1].Info);
				_agentsInMission.Add(_agentsTable[region][2].Info);
				_agentsInMission.Add(_agentsTable[rightRegion][1].Info);
				_agentsInMission.Add(_agentsTable[rightRegion][2].Info);
				_agentsInMission.Add(_agentsTable[leftRegion][1].Info);
				_agentsInMission.Add(_agentsTable[leftRegion][2].Info);

				for (int i = 0; i < _gameConfig.regionsCount; i++) 
				{
					Region theRegion = (Region)i;
					_agentsInMission.Add(_agentsTable[theRegion][0].Info);
				}
				break;
		}
	}
}
