using UnityEngine;

public enum ItemNames
{
    Yarn, Spool, PixieDust, Harp, Fabric, Gold, Key
};

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public ItemNames itemName;
    public ChestTypes ChestTypes;
    public Sprite sprite;
    public int Quantity;
}