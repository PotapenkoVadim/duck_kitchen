public class DeliveryCounter: BaseCounter
{
  public static DeliveryCounter Instante {get; private set;}

  private void Awake()
  {
    Instante = this;
  }

  public override void Interact(Player player)
  {
    if (player.HasKitchenObject())
    {
      if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
      {
        DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
        player.GetKitchenObject().DestroySelf();
      }
    }
  }
}