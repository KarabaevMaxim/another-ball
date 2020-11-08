using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Gameplay.Platforms
{
  public class PlatformsSpawner
  {
    #region Зависимости

    private readonly PlatformsPool _pool;
    private readonly GameParams _gameParams;

    #endregion
    
    public IReadOnlyList<PlatformComponent> SpawnOnStart()
    {
      var upPlatforms = SpawnUpOnStart(SpawnUp);
      var downPlatforms = SpawnUpOnStart(SpawnDown);
      var result = new List<PlatformComponent>(upPlatforms.Count + downPlatforms.Count);
      result.AddRange(upPlatforms);
      result.AddRange(downPlatforms);
      return result;
    }

    private IReadOnlyList<PlatformComponent> SpawnUpOnStart(Func<float, PlatformComponent> spawnFunc)
    {
      var result = new List<PlatformComponent>();
      var currentLength = 0.0f;
      var currentX = _gameParams.PlatformsStartX;
      
      while (currentLength <= _gameParams.PlatformsLengthOnStart)
      {
        var platformUp = spawnFunc.Invoke(currentX);
        currentLength += platformUp.Length;
        currentX += platformUp.Length;
        result.Add(platformUp);
      }

      return result;
    }
    
    private PlatformComponent SpawnUp(float x)
    {
      return SpawnInternal(new Vector3(x, _gameParams.PlatformsUpY, 0));
    }
    
    private PlatformComponent SpawnDown(float x)
    {
      return SpawnInternal(new Vector3(x, _gameParams.PlatformsDownY, 0));
    }
    
    private PlatformComponent SpawnInternal(Vector3 position)
    {
      var platform = _pool.Spawn();
      platform.transform.position = position;
      return platform;
    }

    public void Despawn(PlatformComponent platform)
    {
      _pool.Despawn(platform);
    }

    public PlatformsSpawner(PlatformsPool pool, GameParams gameParams)
    {
      _pool = pool;
      _gameParams = gameParams;
    }
  }
}