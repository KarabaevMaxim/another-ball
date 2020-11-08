using UnityEngine;
using Zenject;

namespace Gameplay.Ball
{
  /// <summary>
  /// Фабрика шариков.
  /// </summary>
  public class BallFactory : PlaceholderFactory<Object, BallComponent>
  {
  }
}