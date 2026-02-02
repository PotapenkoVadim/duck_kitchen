using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _moveSpeed = 10f;
  [SerializeField] private float _rotationSpeed = 10f;
  [SerializeField] private GameInput _gameInput;

  private bool _isWalking = false;

  private void Update()
  {
    Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
    Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

    Walk(moveDirection);
    Rotate(moveDirection);
  }

  private void Walk(Vector3 moveDirection)
  {
    transform.position += _moveSpeed * Time.deltaTime * moveDirection;
    _isWalking = moveDirection != Vector3.zero;
  }

  private void Rotate(Vector3 moveDirection)
  {
    if (moveDirection != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
      transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        _rotationSpeed * Time.deltaTime
      );
    }
  }

  public bool IsWalking() =>_isWalking;
}
