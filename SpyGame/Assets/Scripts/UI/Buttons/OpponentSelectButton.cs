using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentSelectButton : ButtonWithSelector 
{
	public PlayerInfo Info { get; private set; }

	[SerializeField] private Text _text;

	public void UpdateInfo(PlayerInfo player)
	{
		Info = player;
		_text.text = string.Format("Player {0}", player.Id);
	}
}
