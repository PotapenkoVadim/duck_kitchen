using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI: MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _counterdownText;

  private void Start()
  {
    KitchenGameManager.Instance.OnStateChanged += HandleGameStateChange;

    Hide();
  }

  private void HandleGameStateChange(object sender, EventArgs e)
  {
    if (KitchenGameManager.Instance.IsCountdownToStartActive())
    {
      Show();
    } else
    {
      Hide();
    }
  }

  private void Update()
  {
    string text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    if (text != _counterdownText.text) _counterdownText.text = text;
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