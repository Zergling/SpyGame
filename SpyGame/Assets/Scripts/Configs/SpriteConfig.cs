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
	[SerializeField] private List<Sprite> _portraits;
	public List<Sprite> Portraits { get { return _portraits; } }

#region Portraits
	public void ClearPortraits()
	{
		_portraits.Clear();
	}

	public void AddPortrait(Sprite portrait)
	{
		_portraits.Add(portrait);
	}
#endregion Portraits
}
