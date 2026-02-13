using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI: MonoBehaviour
{
  [SerializeField] private Image _barImage;
  [SerializeField] private GameObject _hasProgressGameObject;

  private IHasProgress _hasProgress;

  private void Start()
  {
    _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
    if (_hasProgress == null)
    {
      Debug.LogError("Game Object " + _hasProgressGameObject + " does not have a component that implements IHasProgress");
    }

    _hasProgress.OnProgressChanged += HandleProgressChanged;

    _barImage.fillAmount = 0f;
    Hide();
  }

  private void HandleProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
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