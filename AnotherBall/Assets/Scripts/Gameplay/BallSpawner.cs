using UnityEngine;

namespace Gameplay
{
  public class BallSpawner
  {
    private readonly GameParams _gameParams;

    public Ball SpawnOnStart()
    {
      return Object.Instantiate(_gameParams.BallPrefab, _gameParams.BallStartPosition, Quaternion.identity);
    }
    
    public BallSpawner(GameParams gameParams)
    {
      _gameParams = gameParams;
    }
  }
}