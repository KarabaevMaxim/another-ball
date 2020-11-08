using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Application;
using UnityEngine;
using Zenject;

namespace Gameplay.Input
{
  public class MouseInput : IInput
  {
    private EmptyMonoBeh _emptyMonoBeh;
    
    private bool _enabled;
    private Coroutine _coroutine;

    public bool Enabled
    {
      get => _enabled;
      set
      {
        if (_enabled == value)
          return;
        
        _enabled = value;

        if (_enabled)
        {
          _coroutine = _emptyMonoBeh.StartCoroutine(Loop());
        }
        else
        {
          if (_coroutine != null)
          {
            _emptyMonoBeh.StopCoroutine(_coroutine);
            _coroutine = null;
          }
        }
      }
    }

    public event Action OnClick;

    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    private IEnumerator Loop()
    {
      while (true)
      {
        if (UnityEngine.Input.GetMouseButtonDown(0))
          OnClick?.Invoke();

        yield return null;
      }
    }

    [Inject]
    private void Initialize(EmptyMonoBeh emptyMonoBeh)
    {
      _emptyMonoBeh = emptyMonoBeh;
    }
  }
}