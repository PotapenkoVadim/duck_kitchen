using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _moveSpeed = 10f;
  [SerializeField] private float _rotationSpeed = 10f;

  private void Update()
  {
    Vector2 inputVector = new(0, 0);
    
    if (Input.GetKey(KeyCode.W)) inputVector.y += 1;
    if (Input.GetKey(KeyCode.S)) inputVector.y -= 1;
    if (Input.GetKey(KeyCode.A)) inputVector.x -= 1;
    if (Input.GetKey(KeyCode.D)) inputVector.x += 1;

    inputVector = inputVector.normalized;

    Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);
    transform.position += _moveSpeed * Time.deltaTime * moveDirection;

    if (moveDirection != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
      transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        _rotationSpeed * Time.deltaTime
      );
    }

    Debug.Log(inputVector);
  }
}
