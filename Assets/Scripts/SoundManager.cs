using System;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
  public static SoundManager Instance {get; private set;}

  [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

  private void Awake()
  {
    Instance = this;
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

  private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
  {
    PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
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
}