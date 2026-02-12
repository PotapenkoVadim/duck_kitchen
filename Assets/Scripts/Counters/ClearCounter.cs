using UnityEngine;

public class ClearCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO _kitchenObjectSO;

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
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          bool hasAdded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
          if (hasAdded) GetKitchenObject().DestroySelf();
        } else
        {
          if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
          {
            bool hasAdded = plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
            if (hasAdded) player.GetKitchenObject().DestroySelf();
          }
        }
      } else
      {
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }
}
