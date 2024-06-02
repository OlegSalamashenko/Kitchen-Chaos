using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private ResipeListSO resipeListSO;
    private List<ResipeSO> waitRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;
    private void Awake()
    {
        Instance = this;
        waitRecipeSOList = new List<ResipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
           if ( KitcheGameManager.Instance.IsGamePlaying() &&  waitRecipeSOList.Count < waitingRecipesMax) {
                ResipeSO waitingResipeSO = resipeListSO.resipeSOList[UnityEngine.Random.Range(0, resipeListSO.resipeSOList.Count)];
                
                waitRecipeSOList.Add(waitingResipeSO);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) { 
        for (int i = 0; i < waitRecipeSOList.Count; i++)
        {
            ResipeSO waitingRecipeSO = waitRecipeSOList[i];
        
        if (waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
        {
                //has the same numb of ingr
                bool plateContentsMatchesResipe = true;
                foreach ( KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        plateContentsMatchesResipe = false;
                    }
                }
                if (plateContentsMatchesResipe)
                {

                    successfulRecipesAmount++;
                    waitRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return ;
                }
            }      
        }


        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<ResipeSO> GetWaitingRecipeSOList() 
    {
        return waitRecipeSOList; 
    }

    public int GetSuccessfulRecipesAmount() { 
        return successfulRecipesAmount;
    }


}
