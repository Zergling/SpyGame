using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OpponentAgentsWindow : UpdatableWindowBase 
{
	[SerializeField] private Transform _pagesContainer;
	[SerializeField] private List<AgentsPage> _agentsPages;

	private PlayerInfo _activePlayer;
	private List<PlayerInfo> _players;

	public Transform PagesContainer { get { return _pagesContainer; } }
	public List<AgentsPage> AgentsPages { get { return _agentsPages; } }

	[Inject] private GameController _gameController;

	protected override void OnInit()
	{
		for (int i = 0; i < _agentsPages.Count; i++)
			_agentsPages[i].Init();
	}

	protected override void OnUpdateInfo(object info)
	{
		_activePlayer = (PlayerInfo)info;
		var opponents = _gameController.GetOpponents(_activePlayer);
		for (int i = 0; i < _agentsPages.Count; i++)
			_agentsPages[i].UpdateInfo(opponents[i], _activePlayer.Id);
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerWindow>(_activePlayer);
	}
}
