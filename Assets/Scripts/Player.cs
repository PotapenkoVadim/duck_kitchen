using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _moveSpeed = 10f;
  [SerializeField] private float _rotationSpeed = 10f;

  private bool _isWalking = false;

  private void Update()
  {
    Vector2 inputVector = new(0, 0);
    
    if (Input.GetKey(KeyCode.W)) inputVector.y += 1;
    if (Input.GetKey(KeyCode.S)) inputVector.y -= 1;
    if (Input.GetKey(KeyCode.A)) inputVector.x -= 1;
    if (Input.GetKey(KeyCode.D)) inputVector.x += 1;

    inputVector = inputVector.normalized;

    Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);
    Walk(moveDirection);
    Rotate(moveDirection);

    Debug.Log(inputVector);
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
