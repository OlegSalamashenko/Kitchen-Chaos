using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaceCounter
{
    public static event EventHandler OnEnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnEnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnEnyObjectTrashed?.Invoke(this,EventArgs.Empty);

        }
    }
}
