using UnityEngine;

namespace Gameplay.States
{
  public struct UpState : IState
  {
    public IState Switch(Rigidbody rigidbody, float force) => DownState.Create(rigidbody, force);

    public static UpState Create(Rigidbody rigidbody, float force)
    {
      Physics.gravity = Vector3.up;
      rigidbody.velocity = Vector3.up * force;
      return new UpState();
    }
  }
}