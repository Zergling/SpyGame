using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerWindow : UpdatableWindowBase
{
	[SerializeField] private Text _titleText;

	private Player _player;

	[Inject] private GameController _gameController;

	protected override void OnUpdateInfo(object info)
	{
		_player = (Player)info;
		_titleText.text = string.Format("Player {0}", _player.Id);
	}

	public void OnMyAgentsButton()
	{
		Hide();
		_windowsManager.Show<PlayerAgentsWindow>(_player);
	}

	public void OnOppsAgentsButton()
	{
		Hide();
		_windowsManager.Show<OpponentAgentsWindow>(_player);
	}

	public void OnMissionButton()
	{
		Hide();
		_windowsManager.Show<OperationsWindow>(_player);
	}

	public void OnJournalButton()
	{
		Hide();
		_windowsManager.Show<JournalWindow>(_player);
	}

	public void OnEndTurnButton()
	{
		Hide();
		_gameController.EndTurn();
	}
}
