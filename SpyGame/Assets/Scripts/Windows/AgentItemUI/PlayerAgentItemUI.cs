using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAgentItemUI : AgentItemUI 
{
	[Inject] WindowsManager _windowsManager;

	public override void OnClick()
	{
		_back.color = Color.red;
		_windowsManager.GetWindow<PlayerAgentsWindow>().OnItemClicked(this);
	}

	public void Reset()
	{
		_back.color = Color.white;
	}
}
