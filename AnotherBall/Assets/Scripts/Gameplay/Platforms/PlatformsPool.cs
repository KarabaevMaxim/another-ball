using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Platforms
{
  public class PlatformsPool
  {
    private readonly Queue<PlatformComponent> _platforms;
    
    public GameObject Parent { get; }
    
    public PlatformComponent Spawn()
    {
      var result = _platforms.Dequeue();
      result.gameObject.SetActive(true);
      return result;
    }

    public void Despawn(PlatformComponent platform)
    {
      platform.gameObject.SetActive(false);
      _platforms.Enqueue(platform);
    }

    public PlatformsPool(GameParams gameParams, PlatformsFactory factory)
    {
      Parent = new GameObject("Platforms");
      _platforms = new Queue<PlatformComponent>(gameParams.PlatformPoolCapacity);

      for (var i = 0; i < gameParams.PlatformPoolCapacity; i++)
      {
        var platform = factory.Create(gameParams.PlatformPrefab);
        platform.transform.parent = Parent.transform;
        platform.transform.position = Vector3.zero;
        platform.gameObject.SetActive(false);
        _platforms.Enqueue(platform);
      }
    }
  }
}