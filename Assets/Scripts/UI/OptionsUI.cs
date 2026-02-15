
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI: MonoBehaviour
{
  public static OptionsUI Instance {get; private set;}

  [SerializeField] private Button _soundEffectsButton;
  [SerializeField] private Button _musicButton;
  [SerializeField] private Button _closeButton;
  [SerializeField] private TMP_Text _soundEffectsText;
  [SerializeField] private TMP_Text _musicText;

  private void Awake()
  {
    Instance = this;
    _soundEffectsButton.onClick.AddListener(HandleSoundEffectsButtonClick);
    _musicButton.onClick.AddListener(HandleMusicButtonClick);
    _closeButton.onClick.AddListener(HandleCloseButtonClick);
  }

  private void OnDestroy()
  {
    _soundEffectsButton.onClick.RemoveListener(HandleSoundEffectsButtonClick);
    _musicButton.onClick.RemoveListener(HandleMusicButtonClick);
    _closeButton.onClick.RemoveListener(HandleCloseButtonClick);

    KitchenGameManager.Instance.OnGameUnpaused -= HandleGameUnpaused;
  }

  private void Start()
  {
    KitchenGameManager.Instance.OnGameUnpaused += HandleGameUnpaused;
    
    UpdateVisual();
    Hide();
  }

  private void HandleGameUnpaused(object sender, EventArgs e) {}

  private void HandleCloseButtonClick()
  {
    Hide();
  }

  private void HandleSoundEffectsButtonClick()
  {
    SoundManager.Instance.ChangeVolume();
    UpdateVisual();
  }

  private void HandleMusicButtonClick()
  {
    MusicManager.Instance.ChangeVolume();
    UpdateVisual();
  }

  private void UpdateVisual()
  {
    _soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    _musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
  }

  public void Show()
  {
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}