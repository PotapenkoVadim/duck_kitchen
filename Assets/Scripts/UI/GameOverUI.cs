using System;
using TMPro;
using UnityEngine;

public class GameOverUI: MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _recipesDeliveredText;

  private void Start()
  {
    KitchenGameManager.Instance.OnStateChanged += HandleGameStateChange;

    Hide();
  }

  private void OnDestroy()
  {
    KitchenGameManager.Instance.OnStateChanged -= HandleGameStateChange;
  }

  private void HandleGameStateChange(object sender, EventArgs e)
  {
    if (KitchenGameManager.Instance.IsGameOver())
    {
      Show();

      _recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
    } else
    {
      Hide();
    }
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}