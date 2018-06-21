using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RegionSelector : MonoBehaviour
{
	[SerializeField] private Region _region;
	[SerializeField] private Image _back;

	private OperationsWindow _window;

	public void UpdateInfo(OperationsWindow window)
	{
		_window = window;
	}

	public void OnClick()
	{
		_window.OnRegionButtonClick(_region);
	}

	public void FillColor(Region activeRegion)
	{
		if (_region == activeRegion)
			_back.color = Color.red;
		else
			_back.color = Color.white;
	}
}
