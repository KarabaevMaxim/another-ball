using System;
using Gameplay;
using UnityEngine;

[Serializable]
public struct GameParams
{
  [SerializeField]
  private Vector3 _ballStartPosition;
  
  [SerializeField]
  private Ball _ballPrefab;

  public Vector3 BallStartPosition => _ballStartPosition;

  public Ball BallPrefab => _ballPrefab;
}