using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartWindow : WindowBase
{
	[Inject] GameController _gameController;

	public void OnNewGameButton()
	{
		_gameController.StartNewGame();
	}
}