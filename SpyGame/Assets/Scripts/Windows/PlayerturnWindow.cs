using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerTurnWindow : UpdatableWindowBase
{
	[SerializeField] private Text _titleText;

	private PlayerInfo _player;

	[Inject] private GameController _gameController;

	protected override void OnUpdateInfo(object info)
	{
		_player = (PlayerInfo)info;
		_titleText.text = string.Format("Player {0}", _player.Id);
	}

	public void OnMyAgentsButton()
	{
		Hide();
	}

	public void OnOppsAgentsButton()
	{
		Hide();
	}

	public void OnMissionButton()
	{
		Hide();
	}

	public void OnJournalButton()
	{
		Hide();
	}

	public void OnEndTurnButton()
	{
		Hide();
		_gameController.EndTurn();
	}
}
