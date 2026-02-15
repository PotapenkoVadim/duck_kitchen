using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI: MonoBehaviour
{
  [SerializeField] private Button _playButton;
  [SerializeField] private Button _quitButton;

  private void Awake()
  {
    _playButton.onClick.AddListener(HandlePlayButtonClick);
    _quitButton.onClick.AddListener(HandleQuitButtonClick);

    Time.timeScale = 1f;
  }

  private void OnDestroy()
  {
    _playButton.onClick.RemoveListener(HandlePlayButtonClick);
    _quitButton.onClick.RemoveListener(HandleQuitButtonClick);
  }

  private void HandlePlayButtonClick()
  {
    Loader.Load(Loader.Scene.GameScene);
  }

  private void HandleQuitButtonClick()
  {
    Application.Quit();
  }
}