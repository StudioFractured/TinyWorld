using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestTypes
{
    Common, Rare, Epic
}

public class ChestContents : MonoBehaviour
{
    public Item[] Items;
    public GameObject pickupPrefab;
    private Inventory inventory;
    private List<Item> items = new List<Item>();

    [Header("Bounce Force")]
    public Vector2 X_Force = new Vector2(-3f, 3f);
    public Vector2 Y_Force = new Vector2(2f, 6f);
    public ChestTypes chestType;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();

        items.AddRange(Resources.LoadAll<Item>("ItemList"));

        Items = items.ToArray();
    }

    public void OpenChest(GameObject chest, Vector2 DropCount, Vector2 qty, ChestTypes chestType)
    {
        int remainingDrops = Random.Range((int)qty.x, (int)qty.y + 1); // total drop quantity
        int dropCount = Random.Range((int)DropCount.x, (int)DropCount.y + 1); // how many types
        dropCount = Mathf.Min(dropCount, remainingDrops); // don't ask for more types than quantity

        GameObject itemPool = GameObject.FindGameObjectWithTag("ItemPool");
        if (itemPool == null)
        {
            Debug.LogError("No GameObject found with tag 'ItemPool'!");
            return;
        }

        for (int i = 0; i < dropCount; i++)
        {
            GameObject newPickup = Instantiate(pickupPrefab, chest.transform.position, Quaternion.identity);
            InitializeItem init = newPickup.GetComponent<InitializeItem>();

            int length = 0;
            switch (chestType)
            {
                case ChestTypes.Common:
                    length = Items.Length - 2;
                    break;
                case ChestTypes.Rare:
                    length = Items.Length - 1;
                    break;
                case ChestTypes.Epic:
                    length = Items.Length;
                    break;
            }

            init.item = Instantiate(Items[Random.Range(0, length)]);

            // Give this item a portion of the remaining quantity
            int maxQtyForThisDrop = remainingDrops - (dropCount - 1 - i); // leave at least 1 for remaining items
            int quantity = Random.Range(1, maxQtyForThisDrop + 1);
            init.item.Quantity = quantity;
            remainingDrops -= quantity;

            newPickup.transform.SetParent(itemPool.transform);
            StartCoroutine(ApplyForce(newPickup));
        }

        inventory.key -= 1;
        DestroyAllParents(gameObject);
    }

    public void BrokenChest(GameObject chest, Vector2 DropCount, Vector2 qty, ChestTypes chestType)
    {
        int remainingDrops = Random.Range((int)qty.x, (int)qty.y + 1); // total drop quantity
        int dropCount = Random.Range((int)DropCount.x, (int)DropCount.y + 1); // how many types
        dropCount = Mathf.Min(dropCount, remainingDrops); // don't ask for more types than quantity

        GameObject itemPool = GameObject.FindGameObjectWithTag("ItemPool");
        if (itemPool == null)
        {
            Debug.LogError("No GameObject found with tag 'ItemPool'!");
            return;
        }

        for (int i = 0; i < dropCount; i++)
        {
            GameObject newPickup = Instantiate(pickupPrefab, chest.transform.position, Quaternion.identity);
            InitializeItem init = newPickup.GetComponent<InitializeItem>();

            int length = 0;
            switch (chestType)
            {
                case ChestTypes.Common:
                    length = Items.Length - 2;
                    break;
                case ChestTypes.Rare:
                    length = Items.Length - 1;
                    break;
                case ChestTypes.Epic:
                    length = Items.Length;
                    break;
            }

            init.item = Instantiate(Items[Random.Range(0, length)]);

            // Give this item a portion of the remaining quantity
            int maxQtyForThisDrop = remainingDrops - (dropCount - 1 - i); // leave at least 1 for remaining items
            int quantity = Random.Range(1, maxQtyForThisDrop + 1);
            init.item.Quantity = quantity;
            remainingDrops -= quantity;

            newPickup.transform.SetParent(itemPool.transform);
            StartCoroutine(ApplyForce(newPickup));
        }

        DestroyAllParents(gameObject);
    }

    private IEnumerator ApplyForce(GameObject obj)
    {
        if (obj == null)
        {
            yield break;
        }

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.mass = 1f;
        }

        float xForce = Random.Range(X_Force.x, X_Force.y);
        float yForce = Random.Range(Y_Force.x, Y_Force.y);
        rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);

        yield return null;
    }

    private void DestroyAllParents(GameObject obj)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            Transform parent = current.parent;
            Destroy(current.gameObject);
            current = parent;
        }
    }
}
