using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateComplectVisual : MonoBehaviour
{

    [Serializable] public struct KitchenObjectSO_GameObjecy { 
        public KitchenObjectSO kitchenObjectSO;
        public GameObject GameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObjecy> KitchenObjectSOGameObjecyList;
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObjecy kitchenObjectSOGameObjecy in KitchenObjectSOGameObjecyList)
        {
           kitchenObjectSOGameObjecy.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObjecy kitchenObjectSOGameObjecy in KitchenObjectSOGameObjecyList)
        {
            if (kitchenObjectSOGameObjecy.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObjecy.GameObject.SetActive(true);
            }
        }
        
    }
}
