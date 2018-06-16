﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentItemUI : MonoBehaviour
{
	public bool interactable
	{
		get { return _button.interactable; }
		set { _button.interactable = value; }
	}

	[SerializeField] private Button _button;
	[SerializeField] private Image _portrait;
	[SerializeField] protected Image _back;

	public Agent Info { get; private set; }

	public void UpdateInfo(Agent agent)
	{
		Info = agent;
		_portrait.overrideSprite = Info.Portrait;
	}

	public virtual void OnClick() { }
}
