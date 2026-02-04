using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player Instance {get; private set;}

  public event EventHandler<OnSelectedChangedEventArgs> OnSelectedCounterChanged;
  public class OnSelectedChangedEventArgs: EventArgs
  {
    public ClearCounter selectedCounter;
  }

  [Header("Settings")]
  [SerializeField] private float _moveSpeed = 10f;
  [SerializeField] private float _rotationSpeed = 10f;
  [SerializeField] private GameInput _gameInput;
  [SerializeField] private LayerMask _countersLayerMask;

  private bool _isWalking = false;
  private readonly float _playerRadius = .7f;
  private readonly float _playerHeight = 2f;
  private Vector3 _lastIteractDir;
  private Vector3 _moveDirection = Vector3.zero;
  private ClearCounter _selectedCounter;

  private void Awake() {
    if (Instance != null)
      Debug.LogError("There is more than one Player instance");

    Instance = this;
  }

  private void OnEnable()
  {
    _gameInput.OnInteractAction += HandleInteractAction;
  }

  private void OnDisable()
  {
    _gameInput.OnInteractAction -= HandleInteractAction;
  }

  private void Update()
  {
    GetDirection();
    Walk();
    Rotate();
    HandleInteraction();
  }

  private void Walk()
  {
    float moveDistance = _moveSpeed * Time.deltaTime;
    bool canMove = !Physics.CapsuleCast(
      transform.position,
      transform.position + Vector3.up * _playerHeight,
      _playerRadius,
      _moveDirection,
      moveDistance
    );

    if (!canMove)
    {
      Vector3 moveDirX = new Vector3(_moveDirection.x, 0, 0).normalized;

      canMove = !Physics.CapsuleCast(
        transform.position,
        transform.position + Vector3.up * _playerHeight,
        _playerRadius,
        moveDirX,
        moveDistance
      );

      if (canMove)
      {
        _moveDirection = moveDirX;
      } else
      {
        Vector3 moveDirZ = new Vector3(0, 0, _moveDirection.z).normalized;
        canMove = !Physics.CapsuleCast(
          transform.position,
          transform.position + Vector3.up * _playerHeight,
          _playerRadius,
          moveDirZ,
          moveDistance
        );

        if (canMove)
        {
          _moveDirection = moveDirZ;
        } else
        {
          
        }
      }
    }

    if (canMove)
    {
      transform.position += moveDistance * _moveDirection;
      _isWalking = _moveDirection != Vector3.zero;
    }
  }

  private void GetDirection()
  {
    Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
    _moveDirection = new(inputVector.x, 0, inputVector.y);
  }

  private void Rotate()
  {
    if (_moveDirection != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
      transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        _rotationSpeed * Time.deltaTime
      );
    }
  }

  public bool IsWalking() =>_isWalking;

  private void HandleInteraction()
  {
    if (_moveDirection != Vector3.zero)
      _lastIteractDir = _moveDirection;

    float interactDistance = 2f;
    if (Physics.Raycast(
      transform.position,
      _lastIteractDir,
      out RaycastHit raycastHit,
      interactDistance,
      _countersLayerMask
    ))
    {
      if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
        if (clearCounter != _selectedCounter) {
          SetSelectedCounter(clearCounter);
        }
      } else {
        SetSelectedCounter(null);
      }
    } else {
      SetSelectedCounter(null);
    }
  }

  private void HandleInteractAction(object sender, EventArgs e)
  {
    if (_selectedCounter != null)
      _selectedCounter.Interact();
  }

  private void SetSelectedCounter(ClearCounter selectedCounter)
  {
    _selectedCounter = selectedCounter;

    OnSelectedCounterChanged?.Invoke(this, new OnSelectedChangedEventArgs {
      selectedCounter = _selectedCounter
    });
  }
}
