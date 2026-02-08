using System;
using UnityEngine;

public class CuttingCounter: BaseCounter
{
  public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
  public class OnProgressChangedEventArgs: EventArgs
  {
    public float progressNormalized;
  }

  public event EventHandler OnCut;

  [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

  private int cuttingProgress;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      if (player.HasKitchenObject())
      {
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          player.GetKitchenObject().SetKitchenObjectParent(this);
          cuttingProgress = 0;

          CuttingRecipeSO recipe = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

          OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
            progressNormalized = (float)cuttingProgress / recipe.cuttingProgressMax
          });
        }
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
    KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
    if (HasKitchenObject() && HasRecipeWithInput(kitchenObjectSO))
    {
      cuttingProgress++;

      OnCut?.Invoke(this, EventArgs.Empty);
      CuttingRecipeSO recipe = GetCuttingRecipeSOWithInput(kitchenObjectSO);

      OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
        progressNormalized = (float)cuttingProgress / recipe.cuttingProgressMax
      });

      if (cuttingProgress >= recipe.cuttingProgressMax)
      {
        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
      }
    }
  }

  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
  {
    CuttingRecipeSO recipe = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

    if (recipe != null) return recipe.output;

    return null;
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    CuttingRecipeSO recipe = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

    return recipe != null && recipe.output != null;
  }

  private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenObjectSO)
      {
        return cuttingRecipeSO;
      }
    }

    return null;
  }
}