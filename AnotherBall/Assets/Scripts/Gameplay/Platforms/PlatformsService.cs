using UnityEngine;

namespace Gameplay.Platforms
{
  public class PlatformsService
  {
    private const float UpPlatformsY = 5;
    private const float DownPlatformsY = -5;
    private const float PitsLength = 1;
    
    private readonly PlatformsPool _pool;
    private readonly float _platformsSpeed;
    private readonly Vector3 _upSpawnPosition;
    private readonly Vector3 _downSpawnPosition;

    public void SpawnOnStart(float platformsLength)
    {
      var currentLength = 0.0f;
      
      while (currentLength <= platformsLength)
      {
        var platformUp = Spawn(PlatformPositionKind.Up);
        currentLength += platformUp.Length;
      }
    }
    
    public Platform Spawn(PlatformPositionKind positionKind)
    {
      var platform = SpawnInternal();
      platform.Initialize(positionKind, _platformsSpeed);

      platform.transform.position = positionKind == PlatformPositionKind.Down 
        ? _upSpawnPosition 
        : _downSpawnPosition;

      return platform;
    }
    
    public void Despawn(Platform platform)
    {
      _pool.Despawn(platform);
    }

    private Platform SpawnInternal(Vector3 position)
    {
      var platform = _pool.Spawn();
      platform.transform.position = position;
      return platform;
    }

    public PlatformsService(PlatformsPool pool, float platformsSpeed, Transform spawnPoint)
    {
      _pool = pool;
      _platformsSpeed = platformsSpeed;
      _upSpawnPosition = new Vector3(spawnPoint.position.x, UpPlatformsY, spawnPoint.position.y);
      _downSpawnPosition = new Vector3(spawnPoint.position.x, DownPlatformsY, spawnPoint.position.y);
    }
  }
}