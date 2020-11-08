using Gameplay.Platforms;
using UnityEngine;
using Zenject;

namespace Gameplay
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private DespawnTrigger _despawnTrigger;
    
    [SerializeField, HideInInspector]
    private GameObject _platformsSpawnPoint;
    
    private PlatformsSpawner _platformsSpawner;
    
    private void Awake()
    {
      _despawnTrigger = FindObjectOfType<DespawnTrigger>();
      _despawnTrigger.PlatformGone += OnPlatformGone;
    }

    private void OnDestroy()
    {
      _despawnTrigger.PlatformGone -= OnPlatformGone;
    }

    private void OnValidate()
    {
      if (!_despawnTrigger)
        _despawnTrigger = FindObjectOfType<DespawnTrigger>();
      
      if (!_platformsSpawnPoint)
        _platformsSpawnPoint = GameObject.Find("PlatformsSpawnPoint");
    }

    private void OnPlatformGone(PlatformComponent platform)
    {
      _platformsSpawner.Despawn(platform);
    }

    [Inject]
    private void Initialize(PlatformsSpawner platformsSpawner)
    {
      _platformsSpawner = platformsSpawner;
    }
  }
}