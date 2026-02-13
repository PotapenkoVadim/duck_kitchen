using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager: MonoBehaviour
{
  public static DeliveryManager Instance {get; private set;}

  [SerializeField] private RecipeListSO _recipeListSO;

  private List<RecipeSO> _waitingRecipeSOList;
  private float _spawnRecipeTimer;
  private float _spawnRecipeTimerMax = 4f;
  private int _waitingRecipesMax = 4;

  private void Awake()
  {
    Instance = this;
    _waitingRecipeSOList = new List<RecipeSO>();
  }

  private void Update()
  {
    _spawnRecipeTimer -= Time.deltaTime;
    if (_spawnRecipeTimer < 0f)
    {
      _spawnRecipeTimer = _spawnRecipeTimerMax;

      if (_waitingRecipeSOList.Count < _waitingRecipesMax)
      {
        RecipeSO waitingRecipeSo =_recipeListSO.recipeSOList[Random.Range(0, _recipeListSO.recipeSOList.Count)];
        Debug.Log(waitingRecipeSo.recipeName);
        _waitingRecipeSOList.Add(waitingRecipeSo);
      }
    }
  }

  public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < _waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO =  _waitingRecipeSOList[i];

      if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        bool plateContentsMatchesRecipe = true;
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        {
          bool ingredientFound = false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            if (plateKitchenObjectSO == recipeKitchenObjectSO)
            {
              ingredientFound = true;
              break;
            }
          }

          if (!ingredientFound)
          {
            plateContentsMatchesRecipe = false;
          }
        }

        if (plateContentsMatchesRecipe)
        {
          Debug.Log("Player delivered the correct recipe!");
          _waitingRecipeSOList.RemoveAt(i);
          return;
        }
      }
    }

    Debug.Log("Player did not deliver a correct recipe");
  }
}