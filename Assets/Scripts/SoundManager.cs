using System;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
  private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

  public static SoundManager Instance {get; private set;}

  [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

  private float _volume = 1f;

  private void Awake()
  {
    Instance = this;
    _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
  }

  private void Start()
  {
    DeliveryManager.Instance.OnRecipeSuccess += HandleRecipeSuccess;
    DeliveryManager.Instance.OnRecipeFailed += HandleRecipeFailed;
    CuttingCounter.OnAnyCut += HandleCutting;
    Player.Instance.OnPickedSomething += HandlePickUp;
    BaseCounter.OnAnyObjectPlaced += HandlePlaced;
    TrashCounter.OnAnyObjectTrashed += HandleTrashed;
  }

  private void OnDisable()
  {
    DeliveryManager.Instance.OnRecipeSuccess -= HandleRecipeSuccess;
    DeliveryManager.Instance.OnRecipeFailed -= HandleRecipeFailed;
    CuttingCounter.OnAnyCut -= HandleCutting;
    Player.Instance.OnPickedSomething -= HandlePickUp;
    BaseCounter.OnAnyObjectPlaced -= HandlePlaced;
    TrashCounter.OnAnyObjectTrashed -= HandleTrashed;
  }

  private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
  {
    AudioSource.PlayClipAtPoint(audioClip, position, volume);
  }

  private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
  {
    PlaySound(
      audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)],
      position,
      volumeMultiplier * _volume
    );
  }

  private void HandleRecipeSuccess(object sender, EventArgs e)
  {
    DeliveryCounter deliveryCounter = DeliveryCounter.Instante;
    PlaySound(_audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
  }

  private void HandleRecipeFailed(object sender, EventArgs e)
  {
    DeliveryCounter deliveryCounter = DeliveryCounter.Instante;
    PlaySound(_audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
  }

  private void HandleCutting(object sender, EventArgs e)
  {
    CuttingCounter cuttingCounter = sender as CuttingCounter;
    PlaySound(_audioClipRefsSO.chop, cuttingCounter.transform.position);
  }

  private void HandlePickUp(object sender, EventArgs e)
  {
    PlaySound(_audioClipRefsSO.objectPickup, Player.Instance.transform.position);
  }

  private void HandlePlaced(object sender, EventArgs e)
  {
    BaseCounter baseCounter = sender as BaseCounter;
    PlaySound(_audioClipRefsSO.objectDrop, baseCounter.transform.position);
  }

  private void HandleTrashed(object sender, EventArgs e)
  {
    TrashCounter trashCounter = sender as TrashCounter;
    PlaySound(_audioClipRefsSO.trash, trashCounter.transform.position);
  }

  public void PlayFootstepsSound(Vector3 position, float volume)
  {
    PlaySound(_audioClipRefsSO.footstep, position, volume);
  }

  public void ChangeVolume()
  {
    _volume += 0.1f;
    if (_volume > 1.05f) _volume = 0f;

    PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _volume);
    PlayerPrefs.Save();
  }

  public float GetVolume()
  {
    return _volume;
  }

  public void PlayCountdownSound()
  {
    PlaySound(_audioClipRefsSO.warning, Vector3.zero);
  }
}