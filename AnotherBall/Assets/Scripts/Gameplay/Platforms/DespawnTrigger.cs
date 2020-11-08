using System;
using UnityEngine;

namespace Gameplay.Platforms
{
  public class DespawnTrigger : MonoBehaviour
  {
    public event Action<Platform> PlatformGone;
    
    private void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag("Platform"))
        return;

      var platform = other.GetComponent<Platform>();
      PlatformGone?.Invoke(platform);
    }
  }
}