using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSecuritySelector : ButtonWithSelector 
{
	public MissionSecurityLevel SecurityLevel { get; private set; }

	public void UpdateInfo (MissionSecurityLevel level)
	{
		SecurityLevel = level;
	}
}
