using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerTurnWindow : WindowBase
{
	[SerializeField] private Text _titleText;

	private PlayerInfo _player;

	[Inject] private GameController _gameController;

	protected override void OnShowStart()
	{
		_player = _gameController.ActivePlayer;
		_titleText.text = string.Format("Round {0}. Player {1}", _gameController.Round, _player.Id);
	} 

	public void OnMyAgentsButton()
	{
		Hide();
		_windowsManager.Show<PlayerAgentsWindow>();
	}

	public void OnOppsAgentsButton()
	{
		Hide();
		_windowsManager.Show<OpponentAgentsWindow>();
	}

	public void OnMissionButton()
	{
		Hide();
		_windowsManager.Show<MissionWindow>();
	}

	public void OnJournalButton()
	{
		Hide();
		_windowsManager.Show<JournalWindow>();
	}

	public void OnEndTurnButton()
	{
		Hide();
		_gameController.EndTurn();
	}
}
