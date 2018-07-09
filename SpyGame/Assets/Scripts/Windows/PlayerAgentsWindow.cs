using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAgentsWindow : WindowBase
{
	public List<AgentItemUI> AgentItems;

	private PlayerInfo _activePlayer;
	private AgentInfo _swapOne;
	private AgentInfo _swapTwo;

	[Inject] private GameController _gameController;

	protected override void OnShowStart()
	{
		_swapOne = null;
		_swapTwo = null;
		UpdateAgentItems();
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerTurnWindow>();
	}

	private void UpdateAgentItems()
	{
		_activePlayer = _gameController.ActivePlayer;
		var list = _activePlayer.AgentsList;
		for (int i = 0; i < list.Count; i++) 
		{
			var item = AgentItems[i];
			item.UpdateInfo(list[i]);
			item.OnClick.RemoveAllListeners();
			item.OnClick.AddListener(delegate {
				item.SetSelected(true);
				OnAgentItemClick(item);
			});
		}
	}

	private void OnAgentItemClick(AgentItemUI item)
	{
		if (_swapOne == null) 
			_swapOne = item.Info;
		else if (_swapTwo == null) 
		{
			_swapTwo = item.Info;
			_activePlayer.SwapAgents(_swapOne, _swapTwo);
			_swapOne = null;
			_swapTwo = null;
			UpdateAgentItems();
		}
	}
}
