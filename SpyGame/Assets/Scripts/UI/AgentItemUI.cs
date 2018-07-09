﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentItemUI : MonoBehaviour
{
	public Button.ButtonClickedEvent OnClick { get { return _button.onClick; } }

	public bool interactable
	{
		get { return _button.interactable; }
		set { _button.interactable = value; }
	}

	[SerializeField] private Button _button;
	[SerializeField] private Image _portrait;
	[SerializeField] protected Image _back;

	public AgentInfo Info { get; private set; }

	public void UpdateInfo(AgentInfo agent)
	{
		Info = agent;
		_portrait.overrideSprite = Info.Portrait;
		_back.color = Color.white;
	}

	public void UpdateInfo(AgentInfo agent, int viewerId)
	{
		UpdateInfo(agent);
		if (agent.SpyOwner == viewerId)
			_back.color = Color.red;
		else
			_back.color = Color.white;
	}

	public void UpdateInfo(AgentInfo agent, bool isInMission)
	{
		UpdateInfo(agent);
		if (isInMission)
			_back.color = Color.red;
		else
			_back.color = Color.white;
	}

	public void SetSelected(bool selected)
	{
		if (selected)
			_back.color = Color.red;
		else
			_back.color = Color.white;
	}
}
