using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{
  [SerializeField] private ClearCounter _clearCounter;
  [SerializeField] private GameObject _visualGameObject;
  
  private void Start()
  {
    Player.Instance.OnSelectedCounterChanged += HandleSelect;
  }

  private void OnDisable()
  {
    Player.Instance.OnSelectedCounterChanged -= HandleSelect;
  }

  private void HandleSelect(object sender, Player.OnSelectedChangedEventArgs e)
  {
    if (e.selectedCounter == _clearCounter) Show();
    else Hide();
  }

  private void Show() => _visualGameObject.SetActive(true);
  private void Hide() => _visualGameObject.SetActive(false);
}