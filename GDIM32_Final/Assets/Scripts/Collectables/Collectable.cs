using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData _itemData;
    void PickUp()
    {
        Debug.Log("Picked up " + gameObject.name);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            PickUp();
        }
    }
}