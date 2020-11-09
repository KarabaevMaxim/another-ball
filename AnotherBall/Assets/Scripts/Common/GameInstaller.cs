using Gameplay.Ball;
using Gameplay.Input;
using Gameplay.Platforms;
using UI;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Common
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField]
    private GameParams _gameParams = default;

    [SerializeField, HideInInspector]
    private GameScreen _gameScreen;
  
    [SerializeField, HideInInspector]
    private PauseScreen _pauseScreen;
  
    [SerializeField, HideInInspector]
    private WaitingStartScreen _waitingScreen;
    
    [SerializeField, HideInInspector]
    private GameOverScreen _gameOverScreen;

    public override void InstallBindings()
    {
      Container.Bind<GameScreen>()
        .FromInstance(_gameScreen)
        .AsSingle();
    
      Container.Bind<PauseScreen>()
        .FromInstance(_pauseScreen)
        .AsSingle();
    
      Container.Bind<WaitingStartScreen>()
        .FromInstance(_waitingScreen)
        .AsSingle();
      
      Container.Bind<GameOverScreen>()
        .FromInstance(_gameOverScreen)
        .AsSingle();

      Container.Bind<EmptyMonoBeh>()
        .FromNewComponentOnRoot()
        .AsSingle();

      Container.Bind<GameViewModel>()
        .AsSingle()
        .NonLazy();

      Container.Bind<BallSpawner>()
        .AsSingle();
    
      Container.Bind<GameParams>()
        .FromInstance(_gameParams)
        .AsSingle();

      Container.Bind<PlatformsPool>()
        .AsSingle();
    
      Container.Bind<PlatformsSpawner>()
        .AsSingle();
    
      Container
        .BindFactory<Object, BallComponent, BallFactory>()
        .FromFactory<PrefabFactory<BallComponent>>();
    
      Container
        .BindFactory<Object, PlatformComponent, PlatformsFactory>()
        .FromFactory<PrefabFactory<PlatformComponent>>();
    
#if DEBUG
      Container.Bind<IInput>()
        .To<MouseInput>()
        .AsSingle();
#else
      throw new NotImplementedException("Ввод с тачскрина не реализован");
#endif

      SignalBusInstaller.Install(Container);
    }

    private void OnValidate()
    {
      if (!_gameScreen)
        _gameScreen = FindObjectOfType<GameScreen>();
    
      if (!_pauseScreen)
        _pauseScreen = FindObjectOfType<PauseScreen>();
    
      if (!_waitingScreen)
        _waitingScreen = FindObjectOfType<WaitingStartScreen>();
      
      if (!_gameOverScreen)
        _gameOverScreen = FindObjectOfType<GameOverScreen>();
    }
  }
}