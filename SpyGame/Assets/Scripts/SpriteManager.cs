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

		_unusedPortraits = new List<Sprite>(_spriteConfig.portraits);
		_usedPortraits = new List<Sprite>();
	}

	public Sprite GetRandomPortrait()
	{
		int index = Random.Range(0, _unusedPortraits.Count);
		Sprite result = _unusedPortraits[index];
		_unusedPortraits.Remove(result);
		_usedPortraits.Add(result);
		return result;
	}
}
