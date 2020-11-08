using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  /// <summary>
  /// Компонент игрового экрана.
  /// </summary>
  public class GameScreen : MonoBehaviour
  {
    [SerializeField]
    private Text _score;
    
    [SerializeField, HideInInspector]
    private Button _pauseBtn;

    private Action _onClickPauseBtnAction;

    public Action OnClickPauseBtnAction
    {
      get => _onClickPauseBtnAction;
      set
      {
        _onClickPauseBtnAction = value;
        _pauseBtn.onClick.AddListener(() => _onClickPauseBtnAction?.Invoke());
      }
    }

    public string ScoreText
    {
      get => _score.text;
      set => _score.text = value;
    }

    private void OnValidate()
    {
      if (!_pauseBtn)
        _pauseBtn = GetComponentInChildren<Button>();
    }
  }
}