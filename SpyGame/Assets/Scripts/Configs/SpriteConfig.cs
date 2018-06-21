using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteConfigInternal;

namespace SpriteConfigInternal
{
}

public class SpriteConfig : ScriptableObject 
{
	[SerializeField] private List<Sprite> _inspectorPortraits;
	[SerializeField] private List<Sprite> _inspectorBackgrounds;

	private List<Sprite> _portraits;
	private List<Sprite> _backgrounds;

	public void Init()
	{
		_portraits = new List<Sprite>(_inspectorPortraits);
		_backgrounds = new List<Sprite>(_inspectorBackgrounds);
	}

#region Portraits
	public void ClearPortraits()
	{
		_inspectorPortraits.Clear();
	}

	public void AddPortrait(Sprite portrait)
	{
		_inspectorPortraits.Add(portrait);
	}

	public Sprite GetPortrait()
	{
		int index = UnityEngine.Random.Range(0, _portraits.Count);
		Sprite portrait = _portraits[index];
		_portraits.Remove(portrait);
		return portrait;
	}
#endregion Portraits

#region Backgrounds
	public void ClearBackgrounds()
	{
		_inspectorBackgrounds.Clear();
	}

	public void AddBackground(Sprite back)
	{
		_inspectorBackgrounds.Add(back);
	}

	public Sprite GetBackground()
	{
		int index = UnityEngine.Random.Range(0, _backgrounds.Count);
		Sprite back = _backgrounds[index];
		_backgrounds.Remove(back);
		return back;
	}
#endregion Backgrounds
}
