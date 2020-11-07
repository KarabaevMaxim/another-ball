using System;
using States;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
  [SerializeField]
  private float _force;
  
  [SerializeField]
  private IInput _input;
  
  [SerializeField, HideInInspector]
  private Rigidbody _rigidbody;

  private IState _currentState;
  
  private void Awake()
  {
    _input.OnClick += OnClicked;
  }

  private void Start()
  {
    _currentState = DownState.Create(_rigidbody, _force);
  }

  private void OnDestroy()
  {
    _input.OnClick -= OnClicked;
  }
  
  private void OnValidate()
  {
    if (_input == null)
      _input = GetComponent<IInput>();
    
    if (!_rigidbody)
      _rigidbody = GetComponent<Rigidbody>();
  }
  
  private void OnClicked()
  {
    _currentState = _currentState.Switch(_rigidbody, _force);
  }
}