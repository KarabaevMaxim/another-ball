using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Platforms
{
  public class PlatformsPool
  {
    private readonly Queue<Platform> _platforms;
    
    public GameObject Parent { get; }
    
    public Platform Spawn()
    {
      var result = _platforms.Dequeue();
      result.gameObject.SetActive(true);
      return result;
    }

    public void Despawn(Platform platform)
    {
      platform.gameObject.SetActive(false);
      _platforms.Enqueue(platform);
    }

    public PlatformsPool(int poolCapacity, Platform prefab)
    {
      Parent = new GameObject("Platforms");
      _platforms = new Queue<Platform>(poolCapacity);

      for (var i = 0; i < poolCapacity; i++)
      {
        var platform = Object.Instantiate(prefab, Parent.transform);
        platform.transform.position = Vector3.zero;
        platform.gameObject.SetActive(false);
        _platforms.Enqueue(platform);
      }
    }
  }
}