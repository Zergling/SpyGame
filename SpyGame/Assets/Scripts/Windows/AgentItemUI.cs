using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentItemUI : MonoBehaviour
{
	[SerializeField] private Image _portrait;
	[SerializeField] private Image _back;

	private Agent _info;

	public void UpdateInfo(Agent agent)
	{
		_info = agent;
		_portrait.overrideSprite = _info.Portrait;
	}
}
