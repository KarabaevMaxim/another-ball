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
    private Rigidbody _rigidbody;
    private float _speed;

    public float Length => _length;

    private void FixedUpdate()
    {
      _rigidbody.AddForce(Vector3.left * _speed);
    }

    [Inject]
    private void Initialize(GameParams gameParams, Rigidbody rigidbody)
    {
      _speed = gameParams.PlatformsSpeed;
      _rigidbody = rigidbody;
    }
  }
}