using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCount : BaceCounter
{

    public static DeliveryCount Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        { 
            if (player.GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject)) { }

            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            
            player.GetKitchenObject().DestroySelf();
        }
    }
}
