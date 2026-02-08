using UnityEngine;

public class StoveCounterVisual: MonoBehaviour
{
  [SerializeField] private StoveCounter _stoveCounter;
  [SerializeField] private GameObject _stoveOnGameObject;
  [SerializeField] private GameObject _particlesGameObject;

  private void Start()
  {
    _stoveCounter.OnStateChanged += HandleChangeState;
  }

  private void OnDisable()
  {
    _stoveCounter.OnStateChanged -= HandleChangeState;
  }

  private void HandleChangeState(object sender, StoveCounter.OnStateChangedEventArgs e)
  {
    bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
    _stoveOnGameObject.SetActive(showVisual);
    _particlesGameObject.SetActive(showVisual);
  }
}