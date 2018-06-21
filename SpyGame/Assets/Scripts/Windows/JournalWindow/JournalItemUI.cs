using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.Events;

public class JournalItemUI : MonoBehaviour
{
	[SerializeField] private Text _text;
	[SerializeField] private Button _sabotageButton;

	private JournalEntry _entry;

	public void UpdateInfo(JournalEntry entry, UnityAction callback = null)
	{
		_sabotageButton.onClick.RemoveAllListeners();

		_entry = entry;
		if (_entry.JournalOwnerId == _entry.PlayerId || _entry.IsSabotaged)
			_sabotageButton.gameObject.SetActive(false);
		else
			_sabotageButton.gameObject.SetActive(true);

		switch (_entry.entryType) 
		{
			case JournalEntryType.MyMission:
				_text.text = string.Format("I commited a mission in {0}", _entry.Region);
				break;

			case JournalEntryType.OtherPlayerMission:
				_text.text = string.Format("Player {0} commited a mission in {1}", _entry.PlayerId, _entry.Region);
				break;

			case JournalEntryType.Sabotage:
				_text.text = string.Format("Your mission in {0} was sabotaged", _entry.Region);
				_sabotageButton.gameObject.SetActive(false);
				break;

			default:
				break;
		}

		if (callback != null)
			_sabotageButton.onClick.AddListener(callback);
	}
}
