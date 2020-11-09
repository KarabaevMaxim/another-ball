using System;
using Common;

namespace Gameplay.Ball
{
  /// <summary>
  /// Сервис для создания шарика.
  /// </summary>
  public class BallSpawner
  {
    private readonly GameParams _gameParams;
    private readonly BallFactory _factory;

    public BallComponent SpawnOnStart(Action fellAction, Action pitPassedAction)
    {
      var ball = _factory.Create(_gameParams.BallPrefab);
      ball.FellInPitAction = fellAction;
      ball.PitPassedAction = pitPassedAction;
      ball.transform.position = _gameParams.BallStartPosition;
      return ball;
    }
    
    public BallSpawner(GameParams gameParams, BallFactory factory)
    {
      _gameParams = gameParams;
      _factory = factory;
    }
  }
}