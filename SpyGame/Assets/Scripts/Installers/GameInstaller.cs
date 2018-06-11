using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
	[SerializeField] private SpriteConfig _spriteConfig;

	[SerializeField] private GameController _gameController;
	[SerializeField] private WindowsManager _windowsManager;

    public override void InstallBindings()
    {
		Container.BindInstance(_spriteConfig).AsSingle();

		Container.BindFactory<Agent, Agent.Factory>();
		Container.BindFactory<JournalEntry, JournalEntry.Factory>();
		Container.BindFactory<Player, Player.Factory>();

		Container.BindInstance(_windowsManager).AsSingle();
		Container.BindInstance(_gameController).AsSingle();
    }
}