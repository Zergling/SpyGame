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

	private List<Sprite> _portraits;

	public void Init()
	{
		_portraits = new List<Sprite>(_inspectorPortraits);
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
}
