using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData _itemData;
    
    void PickUp()
    {
        Debug.Log("Picked up " + gameObject.name);
        //GameController.Instance.Player.OnItemPickedUp?.Invoke(_itemData);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            PickUp();
        }
    }
}