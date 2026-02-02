using UnityEngine;

public class GameInput : MonoBehaviour
{
  private InputSystem_Actions _inputActions;

  private void Awake()
  {
    _inputActions = new();
    _inputActions.Player.Enable();
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = _inputActions.Player.Move.ReadValue<Vector2>();

    return inputVector.normalized;
  }
}
