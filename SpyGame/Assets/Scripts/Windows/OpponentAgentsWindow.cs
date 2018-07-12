using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OpponentAgentsWindow : WindowBase 
{
	public List<AgentItemUI> AgentItems;
	public List<OpponentSelectButton> OpponentSelectors;

	private PlayerInfo _activePlayer;
	private PlayerInfo _opponentView;

	[Inject] private GameController _gameController;

	protected override void OnShowStart()
	{
		_activePlayer = _gameController.ActivePlayer;
		var opponents = _gameController.GetOpponents(_activePlayer);
		for (int i = 0; i < opponents.Count; i++) 
		{
			var selector = OpponentSelectors[i];
			selector.UpdateInfo(opponents[i]);
			selector.onClick.RemoveAllListeners();
			selector.onClick.AddListener(delegate { OnSelectorClick(selector); });
		}

		OnSelectorClick(OpponentSelectors[0]);
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerTurnWindow>();
	}

	private void OnSelectorClick(OpponentSelectButton selected)
	{
		_opponentView = selected.Info;
		for (int i = 0; i < OpponentSelectors.Count; i++) 
		{
			var selector = OpponentSelectors[i];
			selector.SetSelected(_opponentView == selector.Info, Color.red);
		}
		UpdateAgentItems();
	}

	private void UpdateAgentItems()
	{
		var list = _opponentView.AgentsList;
		for (int i = 0; i < list.Count; i++) 
		{
			var item = AgentItems[i];
			item.UpdateInfo(list[i], _activePlayer.Id);
		}
	}
}
