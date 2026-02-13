using System;
using UnityEngine;

public class BaseCounter: MonoBehaviour, IKitchenObjectParent
{
  public static event EventHandler OnAnyObjectPlaced;

  [SerializeField] private Transform _counterTopPoint;

  private KitchenObject _kitchenObject;

  public virtual void Interact(Player player)
  {
    Debug.LogError("BaseCounter.Interact");
  }

  public virtual void InteractAlternate(Player palyer)
  {
    return;
  }

  public Transform GetKitchenObjectFollowTransform()
  {
    return _counterTopPoint;
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    _kitchenObject = kitchenObject;

    if (kitchenObject != null)
    {
      OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
    }
  }

  public KitchenObject GetKitchenObject()
  {
    return _kitchenObject;
  }

  public void ClearKitchenObject()
  {
    _kitchenObject = null;
  }

  public bool HasKitchenObject()
  {
    return _kitchenObject != null;
  }
}