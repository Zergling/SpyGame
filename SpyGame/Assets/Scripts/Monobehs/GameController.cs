using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour 
{
	[Inject] private WindowsManager _windowsManager;

	private void Awake()
	{
		_windowsManager.Show<StartWindow>();
	}
}
