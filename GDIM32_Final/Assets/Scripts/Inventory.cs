using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<CollectableData> _items = new List<CollectableData>();

    public void AddItem(CollectableData data)
    {
        if (data == null) return;
        _items.Add(data);
        Debug.Log("Added " + data.itemName + " to inventory");
    }

    void Start()
    {
    }

    void Update()
    {
    }
}