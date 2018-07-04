using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSelector : ButtonWithSelector 
{
	public Region Region { get; private set; }

	public void UpdateInfo(Region region)
	{
		Region = region;
	}
}
