using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool 
{
	private JournalPagePool _journalPagePool;

	[Inject] private MapData _mapData;

	public ObjectPool(JournalPagePool journalPagePool)
	{
		_journalPagePool = journalPagePool;
	}

	public JournalPage GetJournalPage()
	{
		return _journalPagePool.Spawn();
	}

	public void ReturnJournalPage(JournalPage page)
	{
		page.transform.SetParent(_mapData.JournalPageContainer);
		_journalPagePool.Despawn(page);
	}

	public class JournalPagePool: MonoMemoryPool<JournalPage> {}
}
