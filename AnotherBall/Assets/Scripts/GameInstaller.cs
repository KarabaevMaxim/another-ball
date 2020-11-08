using Application;
using UI;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
  [SerializeField, HideInInspector]
  private GameScreen _gameScreen;
  
  [SerializeField, HideInInspector]
  private PauseScreen _pauseScreen;
  
  [SerializeField, HideInInspector]
  private WaitingStartScreen _waitingScreen;
  
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

    Container.Bind<EmptyMonoBeh>()
      .FromNewComponentOnRoot()
      .AsSingle();

    Container.Bind<GameViewModel>()
      .AsSingle()
      .NonLazy();
    
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
  }
}