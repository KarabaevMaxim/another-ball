using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class GameScreen : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private Button _btn;

    private Action _onClickPauseBtnAction;

    public Action OnClickPauseBtnAction
    {
      get => _onClickPauseBtnAction;
      set
      {
        _onClickPauseBtnAction = value;
        _btn.onClick.AddListener(() => _onClickPauseBtnAction?.Invoke());
      }
    }

    private void OnValidate()
    {
      if (!_btn)
        _btn = GetComponentInChildren<Button>();
    }
  }
}