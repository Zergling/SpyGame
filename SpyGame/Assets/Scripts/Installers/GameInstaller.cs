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

		Container.BindInstance(_windowsManager).AsSingle();
		Container.BindInstance(_gameController).AsSingle();
    }
}