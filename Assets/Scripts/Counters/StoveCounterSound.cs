using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound: MonoBehaviour
{
  [SerializeField] private StoveCounter _stoveCounter;

  private AudioSource _audioSource;

  private void Awake()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  private void Start()
  {
    _stoveCounter.OnStateChanged += HandleStateChanged;
  }

  private void OnDisable()
  {
    _stoveCounter.OnStateChanged -= HandleStateChanged;
  }

  private void HandleStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
  {
    bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
    if (playSound)
    {
      _audioSource.Play();
    } else
    {
      _audioSource.Pause();
    }
  }
}