using Gameplay.Platforms;
using UnityEngine;

namespace Gameplay
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField, Range(0, 30)]
    private int _platformPoolsCapacity = default;

    [SerializeField]
    private Platform _platformPrefab = default;
    
    [SerializeField, Range(1, 10)]
    private float _platformsSpeed = default;
    
    [SerializeField, HideInInspector]
    private DespawnTrigger _despawnTrigger;
    
    [SerializeField, HideInInspector]
    private GameObject _platformsSpawnPoint;
    
    private PlatformsService _platformsService;
    
    private void Awake()
    {
      _despawnTrigger = FindObjectOfType<DespawnTrigger>();
      _despawnTrigger.PlatformGone += OnPlatformGone;
      var pool = new PlatformsPool(_platformPoolsCapacity, _platformPrefab);
      _platformsService = new PlatformsService(pool, _platformsSpeed, _platformsSpawnPoint.transform);
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

    private void OnPlatformGone(Platform platform)
    {
      _platformsService.Despawn(platform);
      _platformsService.Spawn(platform.PositionKind);
    }

    public void Pause()
    {
      Time.timeScale = 0;
    }
    
    public void Resume()
    {
      Time.timeScale = 1;
    }
  }
}