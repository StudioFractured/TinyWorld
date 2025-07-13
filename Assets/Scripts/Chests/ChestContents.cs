using System.Collections;
using UnityEngine;

public enum ChestTypes
{
    Common, Rare, Epic
}

public class ChestContents : MonoBehaviour
{
    public Item[] Items;
    public GameObject pickupPrefab;

    [Header("Bounce Force")]
    public Vector2 X_Force = new Vector2(-3f, 3f);
    public Vector2 Y_Force = new Vector2(2f, 6f);
    public ChestTypes chestType;

    public void OpenChest(GameObject chest, Vector2 DropCount, Vector2 qty)
    {
        int dropCount = Random.Range((int)DropCount.x, (int)DropCount.y);

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

            init.item = Instantiate(Items[Random.Range(0, Items.Length)]);
            init.item.Quantity = Random.Range((int)qty.x, (int)qty.y);

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
