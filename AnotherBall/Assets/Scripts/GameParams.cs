using System;
using Gameplay;
using Gameplay.Ball;
using Gameplay.Platforms;
using UnityEngine;

[Serializable]
public struct GameParams
{
  [SerializeField]
  private Vector3 _ballStartPosition;
  
  [SerializeField]
  private BallComponent _ballPrefab;
  
  [SerializeField, Range(0, 30)]
  private int _platformPoolCapacity;

  [SerializeField]
  private PlatformComponent _platformPrefab;
    
  [SerializeField, Range(1, 10)]
  private float _platformsSpeed;

  public Vector3 BallStartPosition => _ballStartPosition;

  public BallComponent BallPrefab => _ballPrefab;

  public int PlatformPoolCapacity => _platformPoolCapacity;

  public PlatformComponent PlatformPrefab => _platformPrefab;

  public float PlatformsSpeed => _platformsSpeed;
}