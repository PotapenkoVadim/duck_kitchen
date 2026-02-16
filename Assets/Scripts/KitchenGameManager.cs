using System;
using UnityEngine;

public class KitchenGameManager: MonoBehaviour
{
  public static KitchenGameManager Instance {get; private set;}

  public event EventHandler OnStateChanged;
  public event EventHandler OnGamePaused;
  public event EventHandler OnGameUnpaused;

  private enum State
  {
    WaitingToStart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  private State _state;
  private float _countdownToStartTimer = 3f;
  private float _gamePlayingTimer;
  private float _gamePlayingTimerMax = 10f;
  private bool _isGamePause = false;

  private void Awake()
  {
    Instance = this;
    _state = State.WaitingToStart;
  }

  private void Start()
  {
    GameInput.Instance.OnPauseAction += HandlePauseAction;
    GameInput.Instance.OnInteractAction += HandleInteractAction;
  }

  private void OnDestroy()
  {
    GameInput.Instance.OnPauseAction -= HandlePauseAction;
  }

  private void HandlePauseAction(object sender, EventArgs e)
  {
    TogglePauseGame();
  }

  private void Update()
  {
    switch (_state)
    {
      case State.WaitingToStart:
        break;

      case State.CountdownToStart:
        _countdownToStartTimer -= Time.deltaTime;
        if (_countdownToStartTimer < 0f)
        {
          _state = State.GamePlaying;
          _gamePlayingTimer = _gamePlayingTimerMax;
          OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        break;

      case State.GamePlaying:
        _gamePlayingTimer -= Time.deltaTime;
        if (_gamePlayingTimer < 0f)
        {
          _state = State.GameOver;
          OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        break;

      case State.GameOver:
        break;
    }
  }

  public bool IsGamePlaying()
  {
    return _state == State.GamePlaying;
  }

  public bool IsCountdownToStartActive()
  {
    return _state == State.CountdownToStart;
  }

  public float GetCountdownToStartTimer()
  {
    return _countdownToStartTimer;
  }

  public bool IsGameOver()
  {
    return _state == State.GameOver;
  }

  public float GetGamePlayingTimerNormalized()
  {
    return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
  }

  public void TogglePauseGame()
  {
    _isGamePause = !_isGamePause;

    if (_isGamePause) {
      Time.timeScale = 0f;
      OnGamePaused?.Invoke(this, EventArgs.Empty);
    } else {
      Time.timeScale = 1f;
      OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
  }

  private void HandleInteractAction(object sender, EventArgs e)
  {
    if (_state == State.WaitingToStart)
    {
      _state = State.CountdownToStart;
      OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}