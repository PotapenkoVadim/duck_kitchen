using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual: MonoBehaviour
{
  [Serializable]
  public struct KitchenObjectSO_GameObject
  {
    public KitchenObjectSO kitchenObjectSO;
    public GameObject gameObject;
  }

  [SerializeField] private PlateKitchenObject _plateKitchenObject;
  [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjects;

  private void Start()
  {
    _plateKitchenObject.OnIngredientAdded += HandleIngredientAdded;
    foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjects)
    {
      kitchenObjectSOGameObject.gameObject.SetActive(false);
    }
  }

  private void OnDisable()
  {
    _plateKitchenObject.OnIngredientAdded -= HandleIngredientAdded;
  }

  private void HandleIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
  {
    foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjects)
    {
      if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
      {
        kitchenObjectSOGameObject.gameObject.SetActive(true);
      }
    }
  }
}