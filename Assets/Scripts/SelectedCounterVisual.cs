using UnityEngine;

public class SelectedCounterVisual: MonoBehaviour
{
  [SerializeField] private BaseCounter _baseCounter;
  [SerializeField] private GameObject[] _visualGameObjectArray;
  
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
    if (e.selectedCounter == _baseCounter) Show();
    else Hide();
  }

  private void Show() {
    foreach (GameObject visualGameObject in _visualGameObjectArray)
    {
      visualGameObject.SetActive(true);
    }
  }

  private void Hide() {
    foreach (GameObject visualGameObject in _visualGameObjectArray)
    {
      visualGameObject.SetActive(false);
    }
  }
}