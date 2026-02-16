using System;
using UnityEngine;

public class StoveBurnWarningUI: MonoBehaviour
{
  [SerializeField] private StoveCounter _stoveCounter;

  private void Start()
  {
    _stoveCounter.OnProgressChanged += HandleProgressChanged;

    Hide();
  }

  private void HandleProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
  {
    float burnShowProgressAmount = 0.5f;
    bool show = _stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

    if (show) Show();
    else Hide();
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