using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  private const string PLAYER_PREFS_BINDINGS = "InputBindings";

  public static GameInput Instance {get; private set;}

  public enum Binding
  {
    Move_Up,
    Move_Down,
    Move_Left,
    Move_Right,
    Interact,
    Interact_Alt,
    Pause
  }

  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  public event EventHandler OnPauseAction;

  private InputSystem_Actions _inputActions;

  private void Awake() {
    Instance = this;
    _inputActions = new();

    if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
    {
      _inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
    }
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

  public string GetBindingText(Binding binding)
  {
    return binding switch
    {
      Binding.Move_Up => _inputActions.Player.Move.bindings[1].ToDisplayString(),
      Binding.Move_Down => _inputActions.Player.Move.bindings[2].ToDisplayString(),
      Binding.Move_Left => _inputActions.Player.Move.bindings[3].ToDisplayString(),
      Binding.Move_Right => _inputActions.Player.Move.bindings[4].ToDisplayString(),
      Binding.Pause => _inputActions.Player.Pause.bindings[0].ToDisplayString(),
      Binding.Interact_Alt => _inputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
      _ => _inputActions.Player.Interact.bindings[0].ToDisplayString(),
    };
  }

  public void RebindBinding(Binding binding, Action onActionRebound)
  {
    _inputActions.Player.Disable();

    InputAction inputAction;
    int bindingIndex;
    switch (binding)
    {
      default:
      case Binding.Move_Up:
        inputAction = _inputActions.Player.Move;
        bindingIndex = 1;
        break;
      case Binding.Move_Down:
        inputAction = _inputActions.Player.Move;
        bindingIndex = 2;
        break;
      case Binding.Move_Left:
        inputAction = _inputActions.Player.Move;
        bindingIndex = 3;
        break;
      case Binding.Move_Right:
        inputAction = _inputActions.Player.Move;
        bindingIndex = 4;
        break;
      case Binding.Interact:
        inputAction = _inputActions.Player.Interact;
        bindingIndex = 0;
        break;
      case Binding.Interact_Alt:
        inputAction = _inputActions.Player.InteractAlternate;
        bindingIndex = 0;
        break;
      case Binding.Pause:
        inputAction = _inputActions.Player.Pause;
        bindingIndex = 0;
        break;
    }

    inputAction.PerformInteractiveRebinding(bindingIndex)
      .OnComplete(callback =>
      {
        callback.Dispose();
        _inputActions.Player.Enable();
        onActionRebound();

        PlayerPrefs.SetString(
          PLAYER_PREFS_BINDINGS,
          _inputActions.SaveBindingOverridesAsJson()
        );
        PlayerPrefs.Save();
      })
      .Start();
  }
}
