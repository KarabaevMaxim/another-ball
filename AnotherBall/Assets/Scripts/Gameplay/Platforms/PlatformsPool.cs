using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable Unity.InefficientPropertyAccess

namespace Gameplay.Platforms
{
  public class PlatformsPool
  {
    private readonly Dictionary<PlatformType, Queue<PlatformComponent>> _pools;

    public GameObject Parent { get; }

    public PlatformComponent SpawnRandom()
    {
      // считаем, что всегда будет 2 типа платформ
      var rand = Random.Range(0, 2);
      return Spawn((PlatformType)rand);
    }
    
    public PlatformComponent Spawn(PlatformType platformType)
    {
      var result = _pools[platformType].Dequeue();
      result.gameObject.SetActive(true);
      return result;
    }

    public void Despawn(PlatformComponent platform)
    {
      platform.gameObject.SetActive(false);
      _pools[platform.Type].Enqueue(platform);
    }

    public PlatformsPool(GameParams gameParams, PlatformsFactory factory)
    {
      Parent = new GameObject("Platforms");
      _pools = new Dictionary<PlatformType, Queue<PlatformComponent>>();

      foreach (var prefab in gameParams.PlatformPrefabs)
      {
        if (!_pools.ContainsKey(prefab.Type))
          _pools[prefab.Type] = new Queue<PlatformComponent>(gameParams.PlatformPoolCapacity);
        
        for (var i = 0; i < gameParams.PlatformPoolCapacity; i++)
        {
          var platform = factory.Create(prefab);
          platform.transform.parent = Parent.transform;
          platform.transform.position = Vector3.zero;
          platform.gameObject.SetActive(false);
          _pools[prefab.Type].Enqueue(platform);
        }
      }
    }
  }
}