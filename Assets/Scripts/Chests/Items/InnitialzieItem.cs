using UnityEngine;

using UnityEngine;

public class InitializeItem : MonoBehaviour
{
    public Item item;
    private SpriteRenderer spriteRenderer;
    public int qty;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (item != null)
        {
            spriteRenderer.sprite = item.sprite;
            gameObject.name = item.itemName.ToString();
            qty = item.Quantity;
        }
    }
}

