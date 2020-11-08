using System;
using UnityEngine;

namespace Gameplay.Platforms
{
  public class Platform : MonoBehaviour
  {
    [SerializeField]
    private float _length = default;
    
    [SerializeField, HideInInspector]
    private Rigidbody _rigidbody;
    
    private float _speed;

    public PlatformPositionKind PositionKind { get; private set; }

    public float Length => _length;

    public void Initialize(PlatformPositionKind positionKind, float speed)
    {
      _speed = speed;
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
  }
}