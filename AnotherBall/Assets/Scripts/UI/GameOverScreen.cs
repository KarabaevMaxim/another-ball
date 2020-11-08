using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  /// <summary>
  /// Компонент экрана поражения.
  /// </summary>
  public class GameOverScreen : MonoBehaviour
  {
    [SerializeField]
    private Text _resultText;
    
    [SerializeField, HideInInspector]
    private Button _restartBtn;

    private Action _onClickRestartBtnAction;

    public Action OnClickRestartBtnAction
    {
      get => _onClickRestartBtnAction;
      set
      {
        _onClickRestartBtnAction = value;
        _restartBtn.onClick.AddListener(() => _onClickRestartBtnAction?.Invoke());
      }
    }

    public string ResultText
    {
      get => _resultText.text;
      set => _resultText.text = value;
    }
    
    private void OnValidate()
    {
      if (!_restartBtn)
        _restartBtn = GetComponentInChildren<Button>();
    }
  }
}