using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour 
{
	private Player _activePlayer;
	private List<Player> _players;



	[Inject] private Player.Factory _playerFactory;
	[Inject] private WindowsManager _windowsManager;

	[Inject] private GameConfig _gameConfig;

	private void Awake()
	{
		
	}

	private void Start()
	{
		_windowsManager.Show<StartWindow>();
	}

#region Main
#endregion Main

#region Private
#endregion Private

#region Public
	public void StartNewGame()
	{
		_players = new List<Player>();
		for (int i = 1; i <= _gameConfig.playersCount; i++)
			_players.Add(_playerFactory.Create(i));

		_activePlayer = _players[0];
		_windowsManager.Hide<StartWindow>();
		_windowsManager.Show<PlayerWindow>(_activePlayer);
	}

	public void EndTurn()
	{
		int index = _players.IndexOf(_activePlayer);
		index++;

		if (index >= _players.Count)
			index = 0;

		_activePlayer = _players[index];
		_windowsManager.Show<PlayerWindow>(_activePlayer);
	}
#endregion Public
}
