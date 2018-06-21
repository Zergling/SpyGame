using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Region
{
	Unknown 		= -1,
	SouthAmerica 	= 0,
	Africa			= 1,
	Asia 			= 2,
	Europe 			= 3,
	NearEast 		= 4,
}

public enum JournalEntryType
{
	Unknown = -1,
	MyMission = 0,
	OtherPlayerMission = 1,
	Sabotage = 2,
}