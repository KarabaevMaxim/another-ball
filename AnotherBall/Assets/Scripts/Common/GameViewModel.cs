using System.Collections;
using System.Collections.Generic;
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
    private readonly GameParams _gameParams;

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

    private BallComponent _currentBall;
    private List<PlatformComponent> _currentPlatforms;

    /// <summary>
    /// Запускает ожидание ввода от пользователя для старта режима игры.
    /// </summary>
    private void StartWaitingStartGame()
    {
      Time.timeScale = 0;
      Score = 0;
      _gameOverScreen.gameObject.SetActive(false);
      _waitingStartScreen.gameObject.SetActive(true);
      _currentBall = _ballSpawner.SpawnOnStart(GameOver, () => Score++);
      _currentPlatforms = _platformsSpawner.SpawnOnStartGame();
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
      _input.Enabled = false;
    }

    private void Restart()
    {
      Debug.Log("Рестарт");
      _ballSpawner.Despawn(_currentBall);
      _platformsSpawner.Despawn(_currentPlatforms);
      _currentPlatforms = null;
      StartWaitingStartGame();
    }

    #region Обработчики событий

    private void OnInputClicked() =>
      Physics.gravity = Physics.gravity == Vector3.down
        ? Vector3.up
        : Vector3.down;

    private void OnDespawnTrigger(PlatformComponent platform)
    {
      _currentPlatforms.Remove(platform);
      _platformsSpawner.Despawn(platform);
    }

    private void OnSpawnTrigger(PlatformComponent platform) =>
      _currentPlatforms.Add(_platformsSpawner.SpawnOnStartPos(platform.transform.position.y));

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
      GameParams gameParams)
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
      _gameParams = gameParams;

      _waitingStartScreen.gameObject.SetActive(false);
      _gameScreen.gameObject.SetActive(false);
      _pauseScreen.gameObject.SetActive(false);
      _gameOverScreen.gameObject.SetActive(false);

      _gameScreen.OnClickPauseBtnAction += Pause;
      _pauseScreen.OnClickPlayBtnAction += Unpause;
      _gameOverScreen.OnClickRestartBtnAction += Restart;
      
      _input.Enabled = false;
      _input.OnClick += OnInputClicked;

      Application.targetFrameRate = 30;

      _gameParams.DespawnTrigger.PlatformTriggered += OnDespawnTrigger;
      _gameParams.SpawnTrigger.PlatformTriggered += OnSpawnTrigger;

      StartWaitingStartGame();
    }
  }
}