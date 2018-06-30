using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour 
{
	public int Turn { get; private set; }
	public PlayerInfo ActivePlayer { get; private set; }

	private List<PlayerInfo> _players;

	// injects
	[Inject] private WindowsManager _windowsManager;
	[Inject] private PlayerInfo.Factory _playerFactory;
	[Inject] private JournalEntry.Factory _journalEntryFactory;

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
	private void SpawnSpies()
	{
		for (int i = 0; i < _players.Count; i++) 
		{
			var player = _players[i];
			var opponents = GetOpponents(player);

			int index;
			PlayerInfo rankOneSpy, rankTwoSpy;

			index = Random.Range(0, opponents.Count);
			rankOneSpy = opponents[index];
			opponents.Remove(rankOneSpy);

			index = Random.Range(0, opponents.Count);
			rankTwoSpy = opponents[index];

			var rankOneAgents = rankOneSpy.GetNonSpyAgents(0);
			index = Random.Range(0, rankOneAgents.Count);
			rankOneAgents[index].SetSpy(player.Id);

			var rankTwoAgents = rankTwoSpy.GetNonSpyAgents(1);
			index = Random.Range(0, rankTwoAgents.Count);
			rankTwoAgents[index].SetSpy(player.Id);
		}
	}

	private PlayerInfo GetPlayer(int id)
	{
		for (int i = 0; i < _players.Count; i++) 
		{
			var player = _players[i];
			if (player.Id == id)
				return player;
		}

		throw new UnityException("player with id " + id.ToString() + " does not exist");
		return null;
	}
#endregion Private

#region Public
	public void StartNewGame()
	{
		_players = new List<PlayerInfo>();
		for (int i = 1; i <= _gameConfig.playersCount; i++) 
		{
			PlayerInfo player = _playerFactory.Create(i);
			_players.Add(player);
		}

		SpawnSpies();
		ActivePlayer = _players[0];
		_windowsManager.Hide<StartWindow>();
		_windowsManager.Show<PlayerWindow>();
	}

	public void EndTurn()
	{
		int index = _players.IndexOf(ActivePlayer);
		index++;

		if (index >= _players.Count)
			index = 0;

		ActivePlayer = _players[index];
		_windowsManager.Show<PlayerWindow>(ActivePlayer);
	}

	public List<PlayerInfo> GetOpponents(PlayerInfo player)
	{
		List<PlayerInfo> opps = new List<PlayerInfo>();
		opps.AddRange(_players);
		opps.Remove(player);
		return opps;
	}
#endregion Public
}
