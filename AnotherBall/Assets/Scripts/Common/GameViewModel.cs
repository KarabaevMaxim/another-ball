using System.Collections;
using Gameplay.Ball;
using Gameplay.Input;
using Gameplay.Platforms;
using UI;
using UnityEngine;
using Zenject;

namespace Common
{
  /// <summary>
  /// Вьюмодель единственной сцены в приложении.
  /// </summary>
  public class GameViewModel
  {
    #region Экраны

    private readonly PauseScreen _pauseScreen;
    private readonly GameScreen _gameScreen;
    private readonly WaitingStartScreen _waitingStartScreen;
    private readonly GameOverScreen _gameOverScreen;

    #endregion

    #region Зависимости

    private readonly EmptyMonoBeh _emptyMonoBeh;
    private readonly BallSpawner _ballSpawner;
    private readonly PlatformsSpawner _platformsSpawner;
    private readonly IInput _input;
    private readonly SignalBus _signalBus;
    
    #endregion

    private int _score;
    
    private int Score
    {
      get => _score;
      set
      {
        _score = value;
        _gameScreen.ScoreText = _score.ToString();
      }
    }

    /// <summary>
    /// Запускает ожидание ввода от пользователя для старта режима игры.
    /// </summary>
    private void StartWaitingStartGame()
    {
      Debug.Log("Ждем ввода от игрока");
      Time.timeScale = 0;
      _waitingStartScreen.gameObject.SetActive(true);
      _ballSpawner.SpawnOnStart();
      _platformsSpawner.SpawnOnStart();
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
      _input.Enabled = true;
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
      _input.Enabled = false;
    }

    /// <summary>
    /// Возвращает приложение из режима паузы.
    /// </summary>
    private void Unpause()
    {
      Time.timeScale = 1;
      _gameScreen.gameObject.SetActive(true);
      _pauseScreen.gameObject.SetActive(false);
      _input.Enabled = true;
    }

    private void GameOver()
    {
      Time.timeScale = 0;
      _gameScreen.gameObject.SetActive(false);
      _gameOverScreen.gameObject.SetActive(true);
      _gameOverScreen.ResultText = $"Результат: {Score}";
    }

    private void Restart()
    {
      Debug.Log("Рестарт");
    }

    #region Обработчики событий

    private void OnInputClicked()
    {
      Physics.gravity = Physics.gravity == Vector3.down
        ? Vector3.up
        : Vector3.down;
    }
    
    private void OnPlatformTriggered(PlatformComponent platform)
    {
      _platformsSpawner.Despawn(platform);
    }

    #endregion

    public GameViewModel(SignalBus signalBus, 
      GameScreen gameScreen, 
      WaitingStartScreen waitingStartScreen, 
      PauseScreen pauseScreen, 
      EmptyMonoBeh emptyMonoBeh, 
      BallSpawner ballSpawner,
      PlatformsSpawner platformsSpawner,
      IInput input,
      GameOverScreen gameOverScreen,
      DespawnTrigger despawnTrigger)
    {
      _signalBus = signalBus;
      _gameScreen = gameScreen;
      _waitingStartScreen = waitingStartScreen;
      _pauseScreen = pauseScreen;
      _emptyMonoBeh = emptyMonoBeh;
      _ballSpawner = ballSpawner;
      _platformsSpawner = platformsSpawner;
      _input = input;
      _gameOverScreen = gameOverScreen;

      _waitingStartScreen.gameObject.SetActive(false);
      _gameScreen.gameObject.SetActive(false);
      _pauseScreen.gameObject.SetActive(false);

      _gameScreen.OnClickPauseBtnAction += Pause;
      _pauseScreen.OnClickPlayBtnAction += Unpause;
      _gameOverScreen.OnClickRestartBtnAction += Restart;
      
      _input.Enabled = false;
      _input.OnClick += OnInputClicked;

      Application.targetFrameRate = 30;

      despawnTrigger.PlatformTriggered += OnPlatformTriggered;
      
      StartWaitingStartGame();
    }
  }
}