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
    
    private PlatformsService _platformsService;
    
    private void Awake()
    {
      _despawnTrigger = FindObjectOfType<DespawnTrigger>();
      _despawnTrigger.PlatformGone += OnPlatformGone;
    }

    private void Start()
    {
      _platformsService.SpawnOnStart(20);
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
      _platformsService.Despawn(platform);
      _platformsService.Spawn(platform.PositionKind);
    }

    [Inject]
    private void Initialize(PlatformsService platformsService)
    {
      _platformsService = platformsService;
    }
  }
}