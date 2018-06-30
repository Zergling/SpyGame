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
	private PlayerInfo _player;
	private Region _region;
	private int _level;
	private List<AgentInfo> _agentsInMission;

	private SabotagePrepareInfo _sabotageInfo;

	[Inject] private GameController _gameController;
	[Inject] private GameConfig _gameConfig;

	public List<AgentItemUI> Agents { get { return _agents; } }
	public List<RegionSelector> RegionSelectors { get { return _regionSelectors; } }
	public Transform RegionSelectorsContainer { get { return _regionSelectorsContainer; } }
	public Transform AgentsContainer { get { return _agentsContainer; } }

	protected override void OnInit()
	{
		_region = Region.Unknown;
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

		_agentsInMission = new List<AgentInfo>();
		for (int i = 0; i < _regionSelectors.Count; i++)
			_regionSelectors[i].UpdateInfo(this);
	}

	protected override void OnUpdateInfo(object info)
	{
		if (info is PlayerInfo) 
		{
			Debug.Log("info is Player");
			_sabotageInfo = null;
			_player = (PlayerInfo)info;
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
		else if (info is SabotagePrepareInfo)
		{
			Debug.Log("info is SabotagePrepareInfo");
			_sabotageInfo = (SabotagePrepareInfo)info;
			_player = _sabotageInfo.Player;
			_region = _sabotageInfo.Entry.Region;
			_level = 0;

			for (int i = 0; i < _regionSelectors.Count; i++)
				_regionSelectors[i].FillColor(_region);
			
			UpdateAgentsInMission(_region, _level);
			UpdateAgentsTable();
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
		if (_region == Region.Unknown)
			return;

		UpdateAgentsInMission(_region, _level);
		UpdateAgentsTable();
	}

	public void OnRegionButtonClick(Region region)
	{
		_region = region;
		UpdateAgentsInMission(_region, _level);
		UpdateAgentsTable();
	}

	public void OnSubmitButton()
	{
		if (_region == Region.Unknown)
			return;

		if (_sabotageInfo != null) 
		{
			_gameController.SubmitSabotage(_player, _sabotageInfo.Entry.PlayerId, _agentsInMission, _region);
			_sabotageInfo.Entry.MarkSabotaged();
		}
		else
			_gameController.SubmitMission(_player, _agentsInMission, _region);
		
		_agentsInMission.Clear();
		_region = Region.Unknown;
		_level = 0;
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

		var agents = _player.Agents;
		switch (level) 
		{
			case 0:
				_agentsInMission.Add(agents[region][0]);
				_agentsInMission.Add(agents[region][1]);
				break;

			case 1:
				_agentsInMission.Add(agents[region][0]);
				_agentsInMission.Add(agents[region][1]);
				_agentsInMission.Add(agents[rightRegion][0]);
				_agentsInMission.Add(agents[leftRegion][0]);
				break;

			case 2:
				_agentsInMission.Add(agents[region][0]);
				_agentsInMission.Add(agents[region][1]);
				_agentsInMission.Add(agents[region][2]);
				_agentsInMission.Add(agents[rightRegion][0]);
				_agentsInMission.Add(agents[rightRegion][1]);
				_agentsInMission.Add(agents[leftRegion][0]);
				_agentsInMission.Add(agents[leftRegion][1]);
				break;

			case 3:
				_agentsInMission.Add(agents[region][1]);
				_agentsInMission.Add(agents[region][2]);
				_agentsInMission.Add(agents[rightRegion][1]);
				_agentsInMission.Add(agents[rightRegion][2]);
				_agentsInMission.Add(agents[leftRegion][1]);
				_agentsInMission.Add(agents[leftRegion][2]);

				for (int i = 0; i < _gameConfig.regionsCount; i++) 
				{
					Region theRegion = (Region)i;
					_agentsInMission.Add(_agentsTable[theRegion][0].Info);
				}
				break;
		}
	}
}
