using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI: MonoBehaviour
{
  [SerializeField] private Button _playButton;
  [SerializeField] private Button _quitButton;

  private void Awake()
  {
    _playButton.onClick.AddListener(HandlePlayButtonClick);
    _quitButton.onClick.AddListener(HandleQuitButtonClick);
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