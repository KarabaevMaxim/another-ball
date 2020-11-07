using UnityEngine;

namespace States
{
  public interface IState
  {
    IState Switch(Rigidbody rigidbody, float force);
  }
}