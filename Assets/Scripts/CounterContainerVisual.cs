using System;
using UnityEngine;

public class CounterContainerVisual: MonoBehaviour
{
  private const string OPEN_CLOSE = "OpenClose";

  [SerializeField] private CounterContainer _counterContainer;

  private Animator _animator;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  private void OnEnable()
  {
    _counterContainer.OnPlayerGrabbedObject += HandlePlayerGrabbedObject;
  }

  private void OnDisable()
  {
    _counterContainer.OnPlayerGrabbedObject -= HandlePlayerGrabbedObject;
  }

  private void HandlePlayerGrabbedObject(object sender, EventArgs e)
  {
    _animator.SetTrigger(OPEN_CLOSE);
  }
}