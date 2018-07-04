using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithSelector : Button 
{
	[SerializeField] private Image _selectionImage;

	public void UpdateSelector(bool selected, Color color)
	{
		if (selected)
			_selectionImage.color = color;
		else
			_selectionImage.color = Color.white;
	}
}