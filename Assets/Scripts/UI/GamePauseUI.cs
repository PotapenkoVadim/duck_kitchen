using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI: MonoBehaviour
{
  [SerializeField] private Button _resumeButton;
  [SerializeField] private Button _mainMenuButton;
  [SerializeField] private Button _optionsButton;

  private void Awake()
  {
    _resumeButton.onClick.AddListener(HandleResumeButtonClick);
    _mainMenuButton.onClick.AddListener(HandleMainMenuButtonClick);
    _optionsButton.onClick.AddListener(HandleOptionsButtonClick);
  }

  private void Start()
  {
    KitchenGameManager.Instance.OnGamePaused += HandleGamePaused;
    KitchenGameManager.Instance.OnGameUnpaused += HandleGameUnpaused;

    Hide();
  }

  private void OnDestroy()
  {
    KitchenGameManager.Instance.OnGamePaused -= HandleGamePaused;
    KitchenGameManager.Instance.OnGameUnpaused -= HandleGameUnpaused;

    _resumeButton.onClick.RemoveListener(HandleResumeButtonClick);
    _mainMenuButton.onClick.RemoveListener(HandleMainMenuButtonClick);
     _optionsButton.onClick.RemoveListener(HandleOptionsButtonClick);
  }

  private void HandleOptionsButtonClick()
  {
    OptionsUI.Instance.Show();
  }

  private void HandleResumeButtonClick()
  {
    KitchenGameManager.Instance.TogglePauseGame();
  }

  private void HandleMainMenuButtonClick()
  {
    Loader.Load(Loader.Scene.MainMenuScene);
  }

  private void HandleGamePaused(object sender, EventArgs e)
  {
    Show();
  }

  private void HandleGameUnpaused(object sender, EventArgs e)
  {
    Hide();
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}