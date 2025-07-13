using UnityEngine;

public enum ItemNames
{
    Yarn, Spool, PixieDust, Harp, Fabric, Gold 
};

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public ItemNames itemName;
    public Sprite sprite;
    public int Quantity;
}
