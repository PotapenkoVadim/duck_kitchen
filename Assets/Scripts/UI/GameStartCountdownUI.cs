using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameStartCountdownUI: MonoBehaviour
{
  private const string NUMBER_POPUP = "NumberPopup";

  [SerializeField] private TextMeshProUGUI _counterdownText;

  private Animator _animator;
  private int _prevNumber;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

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
    int num = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
    if (num != _prevNumber) {
      _prevNumber = num;
      _counterdownText.text = num.ToString();
      _animator.SetTrigger(NUMBER_POPUP);
      SoundManager.Instance.PlayCountdownSound();
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