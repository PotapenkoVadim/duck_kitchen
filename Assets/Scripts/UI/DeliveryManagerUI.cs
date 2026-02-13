using System;
using UnityEngine;

public class DeliveryManagerUI: MonoBehaviour
{
  [SerializeField] private Transform _container;
  [SerializeField] private Transform _recipeTemplate;

  private void Awake()
  {
    _recipeTemplate.gameObject.SetActive(false);
  }

  private void Start()
  {
    DeliveryManager.Instance.OnRecipeSpawned += HandleRecipeSpawned;
    DeliveryManager.Instance.OnRecipeCompleted += HandleRecipeCompleted;

    UpdateVisual();
  }

  private void OnDisable()
  {
    DeliveryManager.Instance.OnRecipeSpawned -= HandleRecipeSpawned;
    DeliveryManager.Instance.OnRecipeCompleted -= HandleRecipeCompleted;
  }

  private void UpdateVisual()
  {
    foreach (Transform child in _container)
    {
      if (child == _recipeTemplate) continue;
      Destroy(child.gameObject);
    }

    foreach(RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSOList())
    {
      Transform recipeTransform = Instantiate(_recipeTemplate, _container);
      recipeTransform.gameObject.SetActive(true);
      recipeTransform.GetComponent<DeliveryManagerSingelUI>().SetRecipeSO(recipeSo);
    }
  }

  private void HandleRecipeSpawned(object sender, EventArgs e)
  {
    UpdateVisual();
  }

  private void HandleRecipeCompleted(object sender, EventArgs e)
  {
    UpdateVisual();
  }
}