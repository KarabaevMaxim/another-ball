using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  /// <summary>
  /// Компонент экрана паузы.
  /// </summary>
  public class PauseScreen : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private Button _playBtn;

    private Action _onClickPlayBtnAction;

    public Action OnClickPlayBtnAction
    {
      get => _onClickPlayBtnAction;
      set
      {
        _onClickPlayBtnAction = value;
        _playBtn.onClick.AddListener(() => _onClickPlayBtnAction?.Invoke());
      }
    }

    private void OnValidate()
    {
      if (!_playBtn)
        _playBtn = GetComponentInChildren<Button>();
    }
  }
}