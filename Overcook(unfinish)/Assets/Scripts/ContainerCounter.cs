using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
