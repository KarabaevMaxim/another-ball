using System;
using UnityEngine;

namespace Gameplay.Input
{
  public class MouseInput : MonoBehaviour, IInput
  {
    public event Action OnClick;

    private void Update()
    {
      if (UnityEngine.Input.GetMouseButtonDown(0))
        OnClick?.Invoke();
    }
  }
}