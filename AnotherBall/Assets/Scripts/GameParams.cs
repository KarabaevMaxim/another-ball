using System;
using Gameplay.Ball;
using Gameplay.Platforms;
using UnityEngine;

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
  private PlatformComponent _platformPrefab;
    
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

  public Vector3 BallStartPosition => _ballStartPosition;

  public BallComponent BallPrefab => _ballPrefab;

  public int PlatformPoolCapacity => _platformPoolCapacity;

  public PlatformComponent PlatformPrefab => _platformPrefab;

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
}