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
	None = -1,
	Mission = 0,
	SpyInfo = 1,
	Sabotage = 2,
}

public enum SecurityLevel
{
	A = 0,
	B = 1,
	C = 2,
	D = 3,
}