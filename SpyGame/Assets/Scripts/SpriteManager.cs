using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpriteManager
{
	private List<Sprite> _unusedPortraits;
	private List<Sprite> _usedPortraits;

	// injects
	private SpriteConfig _spriteConfig;

	public SpriteManager (SpriteConfig spriteConfig)
	{
		_spriteConfig = spriteConfig;
	}

	public Sprite GetRandomPortrait()
	{
		return null;
	}
}
