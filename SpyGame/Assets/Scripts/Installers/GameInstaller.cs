using UnityEngine;
using Zenject;
using ObjectPoolConfigInternal;

public class GameInstaller : MonoInstaller<GameInstaller>
{
	[SerializeField] private SpriteConfig _spriteConfig;
	[SerializeField] private GameConfig _gameConfig;
	[SerializeField] private ObjectPoolConfig _objectPoolConfig;

	[SerializeField] private MapData _mapData;
	[SerializeField] private GameController _gameController;
	[SerializeField] private WindowsManager _windowsManager;

    public override void InstallBindings()
    {
		_spriteConfig.Init();

		Container.BindInstance(_mapData).AsSingle();

		PoolObject poolObject;

		poolObject = _objectPoolConfig.journalPage;
		Container.BindMemoryPool<JournalPage, ObjectPool.JournalPagePool>()
			.WithInitialSize(poolObject.count)
			.FromComponentInNewPrefab(poolObject.poolObject)
			.UnderTransform(_mapData.JournalPageContainer);

		Container.Bind<ObjectPool>().AsSingle();

		Container.BindInstance(_spriteConfig).AsSingle();
		Container.BindInstance(_gameConfig).AsSingle();

		Container.BindFactory<Agent, Agent.Factory>();
		Container.BindFactory<SabotagePrepareInfo, SabotagePrepareInfo.Factory>();
		Container.BindFactory<JournalEntry, JournalEntry.Factory>();
		Container.BindFactory<Player, Player.Factory>();

		Container.BindInstance(_windowsManager).AsSingle();
		Container.BindInstance(_gameController).AsSingle();
    }
}