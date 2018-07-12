using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool 
{
	private JournalPagePool _journalPagePool;

	private MapData _mapData;

	public ObjectPool(MapData mapData, JournalPagePool journalPagePool)
	{
		_mapData = mapData;

		_journalPagePool = journalPagePool;
	}

	public JournalPage GetJournalPage()
	{
		return _journalPagePool.Spawn();
	}

	public void ReturnJournalPage(JournalPage page)
	{
		_journalPagePool.Despawn(page);
		page.transform.SetParent(_mapData.JournalPageContainer);
	}

	public class JournalPagePool : MonoMemoryPool<JournalPage> {}

	public static void BindAll(DiContainer container, ObjectPoolConfig config, MapData mapData)
	{
		container.BindMemoryPool<JournalPage, JournalPagePool>()
			.WithInitialSize(config.journalPage.count)
			.FromComponentInNewPrefab(config.journalPage.poolObject)
			.UnderTransform(mapData.JournalPageContainer);
	}
}
