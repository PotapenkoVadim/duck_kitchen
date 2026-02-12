using System;
using UnityEngine;

public class StoveCounter: BaseCounter, IHasProgress
{
  public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
  public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
  public class OnStateChangedEventArgs: EventArgs
  {
    public State state;
  }

  public enum State
  {
    Idle,
    Frying,
    Fried,
    Burned
  }

  [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;

  private float _fryingTimer;
  private FryingRecipeSO _fryingRecipeSO;
  private State _state;

  private void Start()
  {
    _state = State.Idle;
  }
  
  private void Update()
  {
    if (HasKitchenObject())
    {
      switch (_state)
      {
        case State.Idle:
          break;
        
        case State.Frying:
          _fryingTimer += Time.deltaTime;
          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
          });

          if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
          {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);

            _state = State.Fried;
            _fryingTimer = 0f;
            _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {state = _state});
          }
          break;

        case State.Fried:
          _fryingTimer += Time.deltaTime;
          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
          });

          if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
          {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);
            _state = State.Burned;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {state = _state});
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
              progressNormalized = 0f
            });
          }
          break;

        case State.Burned:
          break;
      }
    }
  }

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      if (player.HasKitchenObject())
      {
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          player.GetKitchenObject().SetKitchenObjectParent(this);
          _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
          _state = State.Frying;
          _fryingTimer = 0f;

          OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {state = _state});
          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
          {
            progressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
          });
        }
      } else
      {
        
      }
    } else
    {
      if (player.HasKitchenObject())
      {
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          bool hasAdded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
          if (hasAdded) {
            GetKitchenObject().DestroySelf();
            _state = State.Idle;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {state = _state});
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
              progressNormalized = 0f
            });
          }
        }
      } else
      {
        GetKitchenObject().SetKitchenObjectParent(player);
        _state = State.Idle;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {state = _state});
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
          progressNormalized = 0f
        });
      }
    }
  }

  private KitchenObjectSO GetOutputForInput<T>(KitchenObjectSO inputKitchenObjectSO)
  {
    FryingRecipeSO recipe = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

    if (recipe != null) return recipe.output;

    return null;
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    FryingRecipeSO recipe = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

    return recipe != null && recipe.output != null;
  }

  private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipeSOArray)
    {
      if (fryingRecipeSO.input == inputKitchenObjectSO)
      {
        return fryingRecipeSO;
      }
    }

    return null;
  }
}