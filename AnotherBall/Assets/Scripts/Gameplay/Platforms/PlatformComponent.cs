using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Platforms
{
  public class PlatformComponent : MonoBehaviour
  {
    [SerializeField]
    private float _length = default;
    
    [SerializeField, HideInInspector]
    private Rigidbody _rigidbody;
    
    private float _speed;

    public PlatformPositionKind PositionKind { get; private set; }

    public float Length => _length;

    public void Initialize(PlatformPositionKind positionKind)
    {
      PositionKind = positionKind;
    }
    
    private void FixedUpdate()
    {
      _rigidbody.AddForce(Vector3.left * _speed);
    }

    private void OnValidate()
    {
      if (!_rigidbody)
        _rigidbody = GetComponent<Rigidbody>();
    }

    [Inject]
    private void Initialize(GameParams gameParams)
    {
      _speed = gameParams.PlatformsSpeed;
    }
  }
}