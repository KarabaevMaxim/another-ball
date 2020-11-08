using UnityEngine.SceneManagement;

namespace Application
{
  public class NavigationManager
  {
    public NavigationManager()
    {
      var sceneName = SceneManager.GetActiveScene().name;
    }
  }
}