using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public static GameInput Instance {get; private set;}

  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  public event EventHandler OnPauseAction;

  private InputSystem_Actions _inputActions;

  private void Awake() {
    Instance = this;
    _inputActions = new();
  }

  private void OnEnable()
  {
    _inputActions.Player.Enable();
    _inputActions.Player.Interact.performed += OnInteractPerformed;
    _inputActions.Player.InteractAlternate.performed += OnInteractAlternatePerformed;
    _inputActions.Player.Pause.performed += OnPausePerformed;
  }

  private void OnDisable()
  {
    _inputActions.Player.Interact.performed -= OnInteractPerformed;
     _inputActions.Player.InteractAlternate.performed -= OnInteractAlternatePerformed;
     _inputActions.Player.Pause.performed -= OnPausePerformed;
     _inputActions.Player.Disable();
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = _inputActions.Player.Move.ReadValue<Vector2>();

    return inputVector.normalized;
  }

  private void OnInteractPerformed(InputAction.CallbackContext obj)
  {
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  private void OnInteractAlternatePerformed(InputAction.CallbackContext obj)
  {
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  private void OnPausePerformed(InputAction.CallbackContext obj)
  {
    OnPauseAction?.Invoke(this, EventArgs.Empty);
  }
}
