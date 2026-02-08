using UnityEngine;

public class CuttingCounter: BaseCounter
{
  [SerializeField] private KitchenObjectSO _cutKitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      if (player.HasKitchenObject())
      {
        player.GetKitchenObject().SetKitchenObjectParent(this);
      } else
      {
        
      }
    } else
    {
      if (player.HasKitchenObject())
      {
        
      } else
      {
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  public override void InteractAlternate(Player palyer)
  {
    if (HasKitchenObject())
    {
      GetKitchenObject().DestroySelf();
      KitchenObject.SpawnKitchenObject(_cutKitchenObjectSO, this);
    }
  }
}