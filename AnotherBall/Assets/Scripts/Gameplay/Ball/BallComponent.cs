using Gameplay.Input;
using UnityEngine;
using Zenject;
// ReSharper disable ParameterHidesMember

namespace Gameplay.Ball
{
  /// <summary>
  /// Главный компонент шарика.
  /// </summary>
  public class BallComponent : MonoBehaviour
  {
    private IInput _input;
    private GameParams _gameParams;
    private Rigidbody _rigidBody;

    private void OnClicked()
    {
      _rigidBody.velocity = Physics.gravity * _gameParams.BallSpeed;
    }

    private void OnDestroy()
    {
      _input.OnClick -= OnClicked;
    }

    [Inject]
    private void Initialize(Rigidbody rigidbody, IInput input, GameParams gameParams)
    {
      _rigidBody = rigidbody;
      _input = input;
      _gameParams = gameParams;
      _input.OnClick += OnClicked;
    }
  }
}