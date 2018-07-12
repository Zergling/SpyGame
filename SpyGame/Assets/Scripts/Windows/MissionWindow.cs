using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissionWindow : UpdatableWindowBase
{
	public List<AgentItemUI> AgentItems;
	public List<RegionSelector> RegionSelectors;
	public List<MissionSecuritySelector> SecuritySelectors;

	private Dictionary<Region, List<AgentItemUI>> _agentsDict;
	private List<AgentInfo> _agentsInMission;
	private Region _region;
	private MissionSecurityLevel _securityLevel;
	private PlayerInfo _activePlayer;

	private MissionInfo _sabotagableMission;

	[Inject] private GameController _gameController;
	[Inject] private GameConfig _gameConfig;

	[Inject] private MissionInfo.Factory _missionFactory;
	[Inject] private JournalEntry.Factory _journalFactory;

	protected override void OnInit()
	{
		_sabotagableMission = null;
		_agentsInMission = new List<AgentInfo>();

		for (int i = 0; i < RegionSelectors.Count; i++) 
		{
			var selector = RegionSelectors[i];
			selector.UpdateInfo((Region)i);
			selector.onClick.RemoveAllListeners();
			selector.onClick.AddListener(delegate { OnRegionButtonClicked(selector); });
		}

		for (int i = 0; i < SecuritySelectors.Count; i++) 
		{
			var selector = SecuritySelectors[i];
			selector.UpdateInfo((MissionSecurityLevel)i);
			selector.onClick.RemoveAllListeners();
			selector.onClick.AddListener(delegate { OnSecurityButtonClicked(selector); });
		}

		int k = 0;
		_agentsDict = new Dictionary<Region, List<AgentItemUI>>();
		for (int i = 0; i < _gameConfig.regionsCount; i++) 
		{
			Region region = (Region)i;
			_agentsDict[region] = new List<AgentItemUI>();
			for (int j = 0; j < _gameConfig.agentsPerRegion; j++) 
			{
				_agentsDict[region].Add(AgentItems[k]);
				k++;
			}
		}
	}

	protected override void OnUpdateInfo(object info)
	{
		_sabotagableMission = (MissionInfo)info;
	}

	protected override void OnShowStart()
	{
		_activePlayer = _gameController.ActivePlayer;

		_securityLevel = MissionSecurityLevel.A;

		if (_sabotagableMission == null) 
			_region = Region.SouthAmerica;
		else 
			_region = _sabotagableMission.Region;

		for (int i = 0; i < RegionSelectors.Count; i++) 
		{
			RegionSelectors[i].SetSelected(_region == RegionSelectors[i].Region, Color.red);
			RegionSelectors[i].interactable = (_sabotagableMission == null);
		}

		for (int i = 0; i < SecuritySelectors.Count; i++)
			SecuritySelectors[i].SetSelected(_securityLevel == SecuritySelectors[i].SecurityLevel, Color.red);
		
		UpdateAgentItems();
	}

	protected override void OnHideStart()
	{
		_sabotagableMission = null;
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerTurnWindow>();
	}

	private void UpdateAgentItems()
	{
		var agents = _activePlayer.AgentsList;
		for (int i = 0; i < agents.Count; i++) 
			AgentItems[i].UpdateInfo(agents[i]);

		UpdateAgentsInMissionList();
		for (int i = 0; i < agents.Count; i++) 
		{
			var agent = agents[i];
			AgentItems[i].UpdateInfo(agent, _agentsInMission.Contains(agent));
		}
	}

	private void OnRegionButtonClicked(RegionSelector clicked)
	{
		_region = clicked.Region;
		for (int i = 0; i < RegionSelectors.Count; i++)
			RegionSelectors[i].SetSelected(_region == RegionSelectors[i].Region, Color.red);
		
		UpdateAgentItems();
	}

	private void OnSecurityButtonClicked(MissionSecuritySelector clicked)
	{
		_securityLevel = clicked.SecurityLevel;
		for (int i = 0; i < SecuritySelectors.Count; i++)
			SecuritySelectors[i].SetSelected(_securityLevel == SecuritySelectors[i].SecurityLevel, Color.red);

		UpdateAgentItems();
	}

	private void UpdateAgentsInMissionList()
	{
		_agentsInMission.Clear();
		int regionInt = (int)_region;
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
		switch (_securityLevel) 
		{
			case MissionSecurityLevel.A:
				_agentsInMission.Add(_agentsDict[_region][0].Info);
				_agentsInMission.Add(_agentsDict[_region][1].Info);
				break;

			case MissionSecurityLevel.B:
				_agentsInMission.Add(_agentsDict[_region][0].Info);
				_agentsInMission.Add(_agentsDict[_region][1].Info);
				_agentsInMission.Add(_agentsDict[rightRegion][0].Info);
				_agentsInMission.Add(_agentsDict[leftRegion][0].Info);
				break;

			case MissionSecurityLevel.C:
				_agentsInMission.Add(_agentsDict[_region][0].Info);
				_agentsInMission.Add(_agentsDict[_region][1].Info);
				_agentsInMission.Add(_agentsDict[_region][2].Info);
				_agentsInMission.Add(_agentsDict[rightRegion][0].Info);
				_agentsInMission.Add(_agentsDict[rightRegion][1].Info);
				_agentsInMission.Add(_agentsDict[leftRegion][0].Info);
				_agentsInMission.Add(_agentsDict[leftRegion][1].Info);
				break;

			case MissionSecurityLevel.D:
				_agentsInMission.Add(_agentsDict[_region][1].Info);
				_agentsInMission.Add(_agentsDict[_region][2].Info);
				_agentsInMission.Add(_agentsDict[rightRegion][1].Info);
				_agentsInMission.Add(_agentsDict[rightRegion][2].Info);
				_agentsInMission.Add(_agentsDict[leftRegion][1].Info);
				_agentsInMission.Add(_agentsDict[leftRegion][2].Info);

				for (int i = 0; i < _gameConfig.regionsCount; i++) 
				{
					Region theRegion = (Region)i;
					_agentsInMission.Add(_agentsDict[theRegion][0].Info);
				}
				break;
		}
	}

	public void OnSubmitButtonClick()
	{
		MissionInfo mission = _missionFactory.Create(_activePlayer.Id, _gameController.Round, _region, _securityLevel);
		_activePlayer.AddMission(mission);
		JournalEntry journalEntry = _journalFactory.Create(mission, JournalEntryType.Mission);
		_activePlayer.AddJournalEntry(journalEntry);

		for (int i = 0; i < _agentsInMission.Count; i++) 
		{
			var agent = _agentsInMission[i];
			if (agent.SpyOwner != -1) 
			{
				var opponent = _gameController.GetPlayer(agent.SpyOwner);
				JournalEntry spyInfo = null;

				if (_sabotagableMission == null)
					spyInfo = _journalFactory.Create(mission, JournalEntryType.SpyInfo);
				else
					spyInfo = _journalFactory.Create(_sabotagableMission, JournalEntryType.SpyInfo);

				opponent.AddJournalEntry(spyInfo);
			}
		}

		if (_sabotagableMission != null) 
		{
			var sabotagedOpponent = _gameController.GetPlayer(_sabotagableMission.PlayerId);
			JournalEntry sabotageEntry = _journalFactory.Create(_sabotagableMission, JournalEntryType.Sabotage);
			sabotagedOpponent.AddJournalEntry(sabotageEntry);
		}

		Hide();
	}
}
