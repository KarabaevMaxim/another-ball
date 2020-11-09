using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    private readonly float _spawnX;
    private PlatformComponent _lastUpPlatform;
    private PlatformComponent _lastDownPlatform;

    public List<PlatformComponent> SpawnOnStartGame()
    {
      var upPlatforms = SpawnUpOnStart(SpawnUp);
      var downPlatforms = SpawnUpOnStart(SpawnDown);
      var result = new List<PlatformComponent>(upPlatforms.Count + downPlatforms.Count);
      result.AddRange(upPlatforms);
      result.AddRange(downPlatforms);
      return result;
    }

    public PlatformComponent SpawnOnStartPos(float yPos)
    {
      return yPos == _gameParams.PlatformsUpY 
        ? SpawnUp(_lastUpPlatform.transform.position.x + _lastUpPlatform.Length) 
        : SpawnDown(_lastDownPlatform.transform.position.x + _lastDownPlatform.Length);
    }
    
    public void Despawn(PlatformComponent platform)
    {
      _pool.Despawn(platform);
    }
    
    public void Despawn(IEnumerable<PlatformComponent> platforms)
    {
      foreach (var platform in platforms)
        Despawn(platform);
    }
    
    private IReadOnlyList<PlatformComponent> SpawnUpOnStart(Func<float, PlatformComponent> spawnFunc)
    {
      var result = new List<PlatformComponent>();
      var currentLength = 0.0f;
      var currentX = _gameParams.PlatformsStartX;
      
      while (currentLength < _gameParams.PlatformsLengthOnStart)
      {
        var platformUp = spawnFunc.Invoke(currentX);
        currentLength += platformUp.Length;
        currentX += platformUp.Length;
        result.Add(platformUp);
      }
      
      var despawnTriggerPos = _gameParams.DespawnTrigger.transform.position;
      _gameParams.DespawnTrigger.transform.position = new Vector3(_spawnX - currentLength, despawnTriggerPos.y, despawnTriggerPos.z);

      return result;
    }
    
    private PlatformComponent SpawnUp(float x)
    {
      _lastUpPlatform = SpawnInternal(new Vector3(x, _gameParams.PlatformsUpY, 0));
      return _lastUpPlatform;
    }
    
    private PlatformComponent SpawnDown(float x)
    {
      _lastDownPlatform = SpawnInternal(new Vector3(x, _gameParams.PlatformsDownY, 0));
      return _lastDownPlatform;
    }
    
    private PlatformComponent SpawnInternal(Vector3 position)
    {
      var platform = _pool.SpawnRandom();
      platform.transform.position = position;
      return platform;
    }

    [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
    public PlatformsSpawner(PlatformsPool pool, GameParams gameParams)
    {
      _pool = pool;
      _gameParams = gameParams;
      _spawnX = _gameParams.PlatformsLengthOnStart + _gameParams.PlatformsStartX;
      var spawnTriggerPos = _gameParams.SpawnTrigger.transform.position;
      _gameParams.SpawnTrigger.transform.position = new Vector3(_spawnX, spawnTriggerPos.y, spawnTriggerPos.z);
      
      _gameParams.UpFallTrigger.transform.position = new Vector3(
        _gameParams.UpFallTrigger.transform.position.x, 
        _gameParams.PlatformsUpY + 1.5f, 
        _gameParams.UpFallTrigger.transform.position.z);
      
      _gameParams.DownFallTrigger.transform.position = new Vector3(
        _gameParams.DownFallTrigger.transform.position.x, 
        _gameParams.PlatformsDownY - .5f,
        _gameParams.DownFallTrigger.transform.position.z);
    }
  }
}