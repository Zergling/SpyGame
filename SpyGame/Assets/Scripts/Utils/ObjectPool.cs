using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool 
{
	[Inject] private MapData _mapData;

	public ObjectPool()
	{
	}

	public void BindAll()
	{
	}
}
