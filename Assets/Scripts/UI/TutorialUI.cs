using System;
using TMPro;
using UnityEngine;

public class Tutorial: MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _moveUpKeyText;
  [SerializeField] private TextMeshProUGUI _moveDownKeyText;
  [SerializeField] private TextMeshProUGUI _moveLeftKeyText;
  [SerializeField] private TextMeshProUGUI _moveRightKeyText;
  [SerializeField] private TextMeshProUGUI _interactKeyText;
  [SerializeField] private TextMeshProUGUI _interactAltKeyText;
  [SerializeField] private TextMeshProUGUI _pauseKeyText;

  private void Start()
  {
    GameInput.Instance.OnBindingRebind += HandleBindingRebind;
    KitchenGameManager.Instance.OnStateChanged += HandleStateChanged;

    UpdateVisual();
    Show();
  }

  private void OnDestroy()
  {
    GameInput.Instance.OnBindingRebind -= HandleBindingRebind;
    KitchenGameManager.Instance.OnStateChanged -= HandleStateChanged;
  }

  private void HandleStateChanged(object sender, EventArgs e)
  {
    if (KitchenGameManager.Instance.IsCountdownToStartActive())
    {
      Hide();
    }
  }

  private void UpdateVisual()
  {
    _moveUpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
    _moveDownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
    _moveLeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
    _moveRightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
    _interactAltKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alt);
    _interactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
    _pauseKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
  }

  private void HandleBindingRebind(object sender, EventArgs e)
  {
    UpdateVisual();
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