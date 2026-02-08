using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI: MonoBehaviour
{
  [SerializeField] private Image _barImage;
  [SerializeField] private CuttingCounter _cuttingCounter;

  private void Start()
  {
    _cuttingCounter.OnProgressChanged += HandleProgressChanged;

    _barImage.fillAmount = 0f;
    Hide();
  }

  private void HandleProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
  {
    _barImage.fillAmount = e.progressNormalized;

    if (e.progressNormalized == 0f || e.progressNormalized == 1f) Hide();
    else Show();
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