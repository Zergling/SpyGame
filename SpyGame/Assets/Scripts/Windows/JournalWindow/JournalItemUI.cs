using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalItemUI : MonoBehaviour 
{
	public Button.ButtonClickedEvent OnClick { get { return _button.onClick; } }
	public JournalEntry Info { get; private set; }

	[SerializeField] private Button _button;
	[SerializeField] private Text _text;

	public void UpdateInfo(JournalEntry info)
	{
		Info = info;
		MissionInfo mission = info.Mission;

		switch (info.EntryType) 
		{
			case JournalEntryType.Mission:
				_text.text = string.Format("Round {0}. I commited a misson in {1}. Secret level is {2}", mission.Round, mission.Region, mission.SecurityLevel);
				_button.gameObject.SetActive(false);
				break;

			case JournalEntryType.SpyInfo:
				_text.text = string.Format("Round {0}. Player {1} commited a misson in {2}.", mission.Round, mission.PlayerId, mission.Region);
				_button.gameObject.SetActive(true);
				break;

			case JournalEntryType.Sabotage:
				_text.text = string.Format("Round {0}. Player {1} sabotaged your misson in {2}.", mission.Round, mission.PlayerId, mission.Region);
				_button.gameObject.SetActive(false);
				break;
		}
	}
}
