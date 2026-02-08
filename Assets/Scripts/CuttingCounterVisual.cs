using System;
using UnityEngine;

public class CuttingCounterVisual: MonoBehaviour
{
  private const string CUT = "Cut";

  [SerializeField] private CuttingCounter _cuttingCounter;

  private Animator _animator;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  private void OnEnable()
  {
    _cuttingCounter.OnCut += HandleCut;
  }

  private void OnDisable()
  {
    _cuttingCounter.OnCut -= HandleCut;
  }

  private void HandleCut(object sender, EventArgs e)
  {
    _animator.SetTrigger(CUT);
  }
}