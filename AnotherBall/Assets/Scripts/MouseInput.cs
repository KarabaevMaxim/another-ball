using System;
using UnityEngine;

public class MouseInput : MonoBehaviour, IInput
{
  public event Action OnClick;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
      OnClick?.Invoke();
  }
}