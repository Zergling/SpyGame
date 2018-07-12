using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JournalWindow : WindowBase 
{
	[SerializeField] private Transform _pageContainer;

	private PlayerInfo _playerInfo;

	private List<JournalPage> _pages;
	private List<JournalItemUI> _items;

	[Inject] private GameController _gameController;
	[Inject] private ObjectPool _objectPool;

	protected override void OnInit()
	{
		_pages = new List<JournalPage>();
		_items = new List<JournalItemUI>();
	}

	protected override void OnShowStart()
	{
		_playerInfo = _gameController.ActivePlayer;

		int pagesCount = _playerInfo.Journal.Count / 4;
		if (_playerInfo.Journal.Count % 4 != 0)
			pagesCount++;

		for (int i = 0; i < pagesCount; i++) 
		{
			var page = _objectPool.GetJournalPage();
			page.transform.SetParent(_pageContainer);
			page.transform.localScale = Vector3.one;
			page.transform.position = Vector3.zero;
			_pages.Add(page);
			_items.AddRange(page.Items);
		}

		UpdateItems();
	}

	protected override void OnHideStart()
	{
		_items.Clear();
		for (int i = 0; i < _pages.Count; i++)
			_objectPool.ReturnJournalPage(_pages[i]);

		_pages.Clear();
	}

	protected override void OnHideEnd()
	{
		_windowsManager.Show<PlayerTurnWindow>();
	}

	private void UpdateItems()
	{
		var journal = _playerInfo.Journal;
		for (int i = 0; i < _items.Count; i++) 
		{
			var item = _items[i];
			item.OnClick.RemoveAllListeners();
			if (i < journal.Count)
			{
				item.gameObject.SetActive(true);
				item.OnClick.AddListener(delegate { OnItemClicked(item); });
				item.UpdateInfo(journal[i]);
			}
			else
				item.gameObject.SetActive(false);
		}
	}

	private void OnItemClicked(JournalItemUI item)
	{
		Hide();
		_windowsManager.Hide<PlayerTurnWindow>();
		_windowsManager.Show<MissionWindow>(item.Info.Mission);
	}
}
