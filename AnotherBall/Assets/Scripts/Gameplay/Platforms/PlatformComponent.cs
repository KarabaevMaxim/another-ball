using System;
using Common;
using UnityEngine;
using Zenject;
// ReSharper disable ParameterHidesMember

namespace Gameplay.Platforms
{
  public class PlatformComponent : MonoBehaviour
  {
    [SerializeField]
    private float _length = default;

    [SerializeField]
    private PlatformType _type = default;
    
    [SerializeField, HideInInspector]
    private Rigidbody _rigidbody;
    private float _speed;

    public float Length => _length;

    public PlatformType Type => _type;

    private void FixedUpdate()
    {
      _rigidbody.MovePosition(transform.position + Vector3.left * (_speed * Time.fixedDeltaTime));
    }

    private void OnValidate()
    {
      if (!_rigidbody)
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    [Inject]
    private void Initialize(GameParams gameParams)
    {
      _speed = gameParams.PlatformsSpeed;
    }
  }
}