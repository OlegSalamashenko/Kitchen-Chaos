using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoveCount : BaceCounter , IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStatateChangedEventArgs> OnStateChanged;

    public class OnStatateChangedEventArgs : EventArgs {
        public State state;
    }
    public enum State { 
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingResipeSO[] fryingResipeSOArray;

    [SerializeField] private BurningResipeSO[] burningResipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingResipeSO fryingResipeSO;
    private BurningResipeSO burningResipeSO;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                   progressNormalized = fryingTimer / fryingResipeSO.fryingTimerMax
                });

                    if (fryingTimer > fryingResipeSO.fryingTimerMax)
                {
                        //fried

                    

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(fryingResipeSO.output, this);


                    state = State.Fried;

                    burningTimer = 0f;
                    
                    burningResipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this,new OnStatateChangedEventArgs { 
                            state = state
                        });
                    }
                    break;
            case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningResipeSO.burningTimerMax
                    });
                   
                    if (burningTimer > burningResipeSO.burningTimerMax)
                    {
                        //Burned

                        GetKitchenObject().DestroySelf();


                        KitchenObject.SpawnKitchenObject(burningResipeSO.output, this);

                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStatateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
            case State.Burned:
                break;
            default:
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
                    fryingResipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;

                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStatateChangedEventArgs
                    {
                        state = state
                    });


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingResipeSO.fryingTimerMax
                    });

                }
            }
            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is hold a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStatateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }


                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStatateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        FryingResipeSO fryingResipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
        return fryingResipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        FryingResipeSO fryingResipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
        if (fryingResipeSO != null)
        {
            return fryingResipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingResipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (FryingResipeSO fryingResipeSO in fryingResipeSOArray)
        {
            if (fryingResipeSO.input == inputkitchenObjectSO)
            {
                return fryingResipeSO;
            }
        }
        return null;
    }
    private BurningResipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (BurningResipeSO burningResipeSO in burningResipeSOArray)
        {
            if (burningResipeSO.input == inputkitchenObjectSO)
            {
                return burningResipeSO;
            }
        }
        return null;
    }

    public bool IsFried() { 
        return state == State.Fried;
    }
}
