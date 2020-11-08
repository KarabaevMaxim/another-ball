using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Application
{
  /// <summary>
  /// Вьюмодель единственной сцены в приложении.
  /// </summary>
  public class GameViewModel
  {
    private readonly PauseScreen _pauseScreen;
    private readonly EmptyMonoBeh _emptyMonoBeh;
    private readonly SignalBus _signalBus;

    private readonly GameScreen _gameScreen;

    private readonly WaitingStartScreen _waitingStartScreen;

    /// <summary>
    /// Запускает ожидание ввода от пользователя для старта режима игры.
    /// </summary>
    private void StartWaitingStartGame()
    {
      Debug.Log("Ждем ввода от игрока");
      Time.timeScale = 0;
      _waitingStartScreen.gameObject.SetActive(true);
      _emptyMonoBeh.StartCoroutine(WaitForKeyDown());
    }

    /// <summary>
    /// Ожидание ввода от пользователя.
    /// </summary>
    private IEnumerator WaitForKeyDown()
    {
      while (!Input.anyKey)
      {
        yield return null;
      }

      StartGame();
    }

    /// <summary>
    /// Запускает режим игры.
    /// </summary>
    private void StartGame()
    {
      Time.timeScale = 1;
      _waitingStartScreen.gameObject.SetActive(false);
      _gameScreen.gameObject.SetActive(true);
      Debug.Log("Игра запущена");
    }
    
    /// <summary>
    /// Переводит приложение в режим паузы.
    /// </summary>
    private void Pause()
    {
      Time.timeScale = 0;
      _gameScreen.gameObject.SetActive(false);
      _pauseScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Возвращает приложение из режима паузы.
    /// </summary>
    private void Unpause()
    {
      Time.timeScale = 1;
      _gameScreen.gameObject.SetActive(true);
      _pauseScreen.gameObject.SetActive(false);
    }

    public GameViewModel(SignalBus signalBus, 
      GameScreen gameScreen, 
      WaitingStartScreen waitingStartScreen, 
      PauseScreen pauseScreen, 
      EmptyMonoBeh emptyMonoBeh)
    {
      _signalBus = signalBus;
      _gameScreen = gameScreen;
      _waitingStartScreen = waitingStartScreen;
      _pauseScreen = pauseScreen;
      _emptyMonoBeh = emptyMonoBeh;
      
      _waitingStartScreen.gameObject.SetActive(false);
      _gameScreen.gameObject.SetActive(false);
      _pauseScreen.gameObject.SetActive(false);

      _gameScreen.OnClickPauseBtnAction += Pause;
      _pauseScreen.OnClickPlayBtnAction += Unpause;
      StartWaitingStartGame();
    }
  }
}