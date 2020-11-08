using System;
using Common;
using UnityEngine;
using Zenject;

namespace Gameplay.Platforms
{
  public class PlatformTrigger : MonoBehaviour
  {
    public event Action<PlatformComponent> PlatformTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag("Platform"))
        return;

      var platform = other.GetComponent<PlatformComponent>();
      PlatformTriggered?.Invoke(platform);
    }

    [Inject]
    private void Initialize(GameParams gameParams)
    {
      transform.localScale = new Vector3(transform.localScale.x, gameParams.PlatformsUpY - gameParams.PlatformsDownY + 1, transform.localScale.z);
    }
  }
}