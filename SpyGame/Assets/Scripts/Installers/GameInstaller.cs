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
		Container.BindInstance(_mapData).AsSingle();

		Container.BindInstance(_spriteConfig).AsSingle();
		Container.BindInstance(_gameConfig).AsSingle();

		Container.Bind<ObjectPool>().AsSingle();

		Container.Bind<SpriteManager>().AsSingle();
		Container.BindFactory<MissionInfo, MissionInfo.Factory>();
		Container.BindFactory<AgentInfo, AgentInfo.Factory>();
		Container.BindFactory<SabotageInfo, SabotageInfo.Factory>();
		Container.BindFactory<JournalEntry, JournalEntry.Factory>();
		Container.BindFactory<PlayerInfo, PlayerInfo.Factory>();

		Container.BindInstance(_windowsManager).AsSingle();
		Container.BindInstance(_gameController).AsSingle();
    }
}