using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Inventory inventory;
    private int quantity;
    private InitializeItem item;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
        item = GetComponent<InitializeItem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (item.gameObject.name)
            {
                case "Fabric":
                    inventory.Fabric += item.qty;
                    break;
                case "Yarn":
                    inventory.Yarn += item.qty;
                    break;
                case "Spool":
                    inventory.Spool += item.qty;
                    break;
                case "PixieDust":
                    inventory.PixieDust += item.qty;
                    break;
                case "Harp":
                    inventory.Harp += item.qty;
                    break;
            }

            Destroy(item.gameObject);
        }
    }
}
