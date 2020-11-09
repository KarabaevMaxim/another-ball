using System;
using Common;
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

    public Action FellInPitAction { get; set; }
    
    public Action PitPassedAction { get; set; }
    
    private void OnClicked()
    {
      _rigidBody.velocity = Physics.gravity * _gameParams.BallSpeed;
    }

    private void OnDestroy()
    {
      _input.OnClick -= OnClicked;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Pit"))
        FellInPitAction?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("PitPassTrigger"))
        PitPassedAction?.Invoke();
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