using UnityEngine;

namespace States
{
  public struct DownState : IState
  {
    public IState Switch(Rigidbody rigidbody, float force) => UpState.Create(rigidbody, force);

    public static DownState Create(Rigidbody rigidbody, float force)
    {
      Physics.gravity = Vector3.down;
      rigidbody.velocity = Vector3.down * force;
      return new DownState();
    }
  }
}