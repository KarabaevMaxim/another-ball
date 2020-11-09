using System;
using System.Collections.Generic;
using Gameplay.Ball;
using Gameplay.Platforms;
using UnityEngine;

namespace Common
{
  /// <summary>
  /// Настройки игры.
  /// </summary>
  [Serializable]
  public struct GameParams
  {
    /// <summary>
    /// Стартовая позиция шарика.
    /// </summary>
    [Header("Настройки шарика")]
    [SerializeField]
    private Vector3 _ballStartPosition;
  
    /// <summary>
    /// Префаб шарика.
    /// </summary>
    [SerializeField]
    private BallComponent _ballPrefab;

    /// <summary>
    /// Скорость перемещения шарика.
    /// </summary>
    [SerializeField]
    private float _ballSpeed;
    
    /// <summary>
    /// Сила притяжения.
    /// </summary>
    [SerializeField]
    private float _gravityStrength;

    /// <summary>
    /// Стартовая позиция платформ по оси X. 
    /// </summary>
    [Header("Настройки платформ")]
    [SerializeField]
    private float _platformsStartX;

    /// <summary>
    /// Позиция верхних платформ по оси Y.
    /// </summary>
    [SerializeField]
    private float _platformsUpY;
  
    /// <summary>
    /// Позиция нижних платформ по оси Y.
    /// </summary>
    [SerializeField]
    private float _platformsDownY;
  
    /// <summary>
    /// Количество экземпляров платформ в пуле.
    /// </summary>
    [SerializeField, Range(0, 100)]
    private int _platformPoolCapacity;

    /// <summary>
    /// Префаб платформы.
    /// </summary>
    [SerializeField]
    private PlatformComponent[] _platformPrefabs;
    
    /// <summary>
    /// Скорость платформы.
    /// </summary>
    [SerializeField, Range(1, 10)]
    private float _platformsSpeed;

    /// <summary>
    /// Суммарная длина платформ на старте.
    /// </summary>
    [SerializeField]
    private int _platformsLengthOnStart;
    
    /// <summary>
    /// Триггер деспауна платформ.
    /// </summary>
    [SerializeField]
    private PlatformTrigger _despawnTrigger;
    
    /// <summary>
    /// Триггер спауна платформ.
    /// </summary>
    [SerializeField]
    private PlatformTrigger _spawnTrigger;

    public Vector3 BallStartPosition => _ballStartPosition;

    public BallComponent BallPrefab => _ballPrefab;

    public int PlatformPoolCapacity => _platformPoolCapacity;

    public IReadOnlyList<PlatformComponent> PlatformPrefabs => _platformPrefabs;

    public float PlatformsSpeed => _platformsSpeed;

    /// <summary>
    /// Стартовая позиция платформ по оси X. 
    /// </summary>
    public float PlatformsStartX => _platformsStartX;

    /// <summary>
    /// Позиция верхних платформ по оси Y.
    /// </summary>
    public float PlatformsUpY => _platformsUpY;

    /// <summary>
    /// Позиция нижних платформ по оси Y.
    /// </summary>
    public float PlatformsDownY => _platformsDownY;

    /// <summary>
    /// Суммарная длина платформ на старте.
    /// </summary>
    public int PlatformsLengthOnStart => _platformsLengthOnStart;

    /// <summary>
    /// Скорость перемещения шарика.
    /// </summary>
    public float BallSpeed => _ballSpeed;

    /// <summary>
    /// Триггер деспауна платформ.
    /// </summary>
    public PlatformTrigger DespawnTrigger => _despawnTrigger;

    /// <summary>
    /// Триггер спауна платформ.
    /// </summary>
    public PlatformTrigger SpawnTrigger => _spawnTrigger;

    /// <summary>
    /// Сила притяжения.
    /// </summary>
    public float GravityStrength => _gravityStrength;
  }
}