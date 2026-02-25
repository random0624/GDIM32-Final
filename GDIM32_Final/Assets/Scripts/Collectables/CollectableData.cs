using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Inventory", menuName = "Data/Item")]
public class CollectableData : ScriptableObject
{
    [Header("Properties")]
    public string itemName;
    public Sprite itemSprite;
    public itemType itemType;
}
public enum itemType{Key, Meat};