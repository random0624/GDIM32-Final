using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Data", menuName = "Collectable Data")]
public class CollectableData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    public int itemValue;
}