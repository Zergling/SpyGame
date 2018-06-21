using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour 
{
	private Player _activePlayer;
	private List<Player> _players;




	[Inject] private WindowsManager _windowsManager;
	[Inject] private Player.Factory _playerFactory;
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
			Player rankOneSpy, rankTwoSpy;

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

	private Player GetPlayer(int id)
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
		_players = new List<Player>();
		for (int i = 1; i <= _gameConfig.playersCount; i++) 
		{
			Player player = _playerFactory.Create(i);
			_players.Add(player);
		}

		SpawnSpies();
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

	public List<Player> GetOpponents(Player player)
	{
		List<Player> opps = new List<Player>();
		opps.AddRange(_players);
		opps.Remove(player);
		return opps;
	}

	public void SubmitMission(Player activePlayer, List<Agent> agentsInMission, Region region)
	{
		activePlayer.AddJournalEntry(_journalEntryFactory.Create(activePlayer.Id, activePlayer.Id, JournalEntryType.MyMission, region));
		for (int i = 0; i < agentsInMission.Count; i++) 
		{
			var agent = agentsInMission[i];
			if (agent.SpyOwner != -1) 
			{
				Player player = GetPlayer(agent.SpyOwner);
				player.AddJournalEntry(_journalEntryFactory.Create(player.Id, activePlayer.Id, JournalEntryType.OtherPlayerMission, region));
			}
		}
	}

	public void SubmitSabotage(Player activePlayer, int sabotagedPlayerId, List<Agent> agentsInMission, Region region)
	{
		Player sabotagedPlayer = GetPlayer(sabotagedPlayerId);
		sabotagedPlayer.AddJournalEntry(_journalEntryFactory.Create(sabotagedPlayer.Id, sabotagedPlayer.Id, JournalEntryType.Sabotage, region));
		SubmitMission(activePlayer, agentsInMission, region);
	}
#endregion Public
}
