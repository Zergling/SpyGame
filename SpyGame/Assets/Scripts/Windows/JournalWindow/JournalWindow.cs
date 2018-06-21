using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JournalWindow : UpdatableWindowBase 
{
	[SerializeField] private Transform _pagesContainer;

	private Player _player;
	private List<JournalPage> _pages;
	private List<JournalItemUI> _items;

	private bool _isRedirected;

	[Inject] private ObjectPool _objectPool;
	[Inject] private GameConfig _gameConfig;

	[Inject] private SabotagePrepareInfo.Factory _sabotageFactory;

	protected override void OnInit()
	{
		base.OnInit();
		_pages = new List<JournalPage>();
		_items = new List<JournalItemUI>();
	}

	protected override void OnUpdateInfo(object info)
	{
		_isRedirected = false;
		_player = (Player)info;
		int entriesCount = _player.Journal.Count;
		int pagesCount = (entriesCount % _gameConfig.journalEntriesPerPage == 0) ? entriesCount / _gameConfig.journalEntriesPerPage : (entriesCount / _gameConfig.journalEntriesPerPage) + 1;
		for (int i = 0; i < pagesCount; i++) 
		{
			var page = _objectPool.GetJournalPage();
			page.transform.SetParent(_pagesContainer);
			page.transform.localScale = Vector3.one;
			page.transform.position = Vector3.zero;
			_pages.Add(page);
			_items.AddRange(page.Entries);
		}

		for (int i = 0; i < _items.Count; i++) 
		{
			if (i < entriesCount) 
			{
				_items[i].gameObject.SetActive(true);
				JournalEntry entry = _player.Journal[i];
				_items[i].UpdateInfo(entry, delegate {
					OnSabotageButtonClick(entry);
				});
			}
			else
				_items[i].gameObject.SetActive(false);
		}
	}

	protected override void OnHideStart()
	{
		for (int i = 0; i < _pages.Count; i++)
			_objectPool.ReturnJournalPage(_pages[i]);

		_pages.Clear();
		_items.Clear();
	}

	protected override void OnHideEnd()
	{
		if (!_isRedirected)
			_windowsManager.Show<PlayerWindow>(_player);
	}

	public void OnSabotageButtonClick(JournalEntry entry)
	{
		_isRedirected = true;
		SabotagePrepareInfo prepareInfo = _sabotageFactory.Create(_player, entry);
		Hide();
		_windowsManager.Show<OperationsWindow>(prepareInfo);
	}
}
