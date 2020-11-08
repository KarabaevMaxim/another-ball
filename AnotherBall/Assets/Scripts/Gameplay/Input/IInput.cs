using System;

namespace Gameplay.Input
{
  public interface IInput
  {
    bool Enabled { get; set; }
    
    event Action OnClick;
  }
}