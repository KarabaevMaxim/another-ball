using UnityEngine;

namespace Gameplay.States
{
  public interface IState
  {
    IState Switch(Rigidbody rigidbody, float force);
  }
}