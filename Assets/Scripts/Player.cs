using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _moveSpeed = 10f;
  [SerializeField] private float _rotationSpeed = 10f;
  [SerializeField] private GameInput _gameInput;

  private bool _isWalking = false;
  private readonly float _playerRadius = .7f;
  private readonly float _playerHeight = 2f;

  private void Update()
  {
    Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
    Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

    Walk(moveDirection);
    Rotate(moveDirection);
  }

  private void Walk(Vector3 moveDirection)
  {
    float moveDistance = _moveSpeed * Time.deltaTime;
    bool canMove = !Physics.CapsuleCast(
      transform.position,
      transform.position + Vector3.up * _playerHeight,
      _playerRadius,
      moveDirection,
      moveDistance
    );

    if (!canMove)
    {
      Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;

      canMove = !Physics.CapsuleCast(
        transform.position,
        transform.position + Vector3.up * _playerHeight,
        _playerRadius,
        moveDirX,
        moveDistance
      );

      if (canMove)
      {
        moveDirection = moveDirX;
      } else
      {
        Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
        canMove = !Physics.CapsuleCast(
          transform.position,
          transform.position + Vector3.up * _playerHeight,
          _playerRadius,
          moveDirZ,
          moveDistance
        );

        if (canMove)
        {
          moveDirection = moveDirZ;
        } else
        {
          
        }
      }
    }

    if (canMove)
    {
      transform.position += moveDistance * moveDirection;
      _isWalking = moveDirection != Vector3.zero;
    }
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
