using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;

public class ChestContents : MonoBehaviour
{
    public Item[] Items;                    // The list of item ScriptableObjects
    public GameObject pickupPrefab;         // The prefab to spawn
    public Transform test;                  // Location to spawn

    [Header("Bounce Force")]
    public Vector2 X_Force;
    public Vector2 Y_Force;

    public bool Spawn = false;

    private void Update()
    {
        if (Spawn)
        {
            CommonChest(test.gameObject);
            //Spawn = false;
        }
    }

    // Common chests will drop 1-3 item types in quantity of 2-4
    public void CommonChest(GameObject chest)
    {
        int dropCount = Random.Range(1, 4);  

        for (int i = 0; i < dropCount; i++)
        {
            GameObject newPickup = Instantiate(pickupPrefab, chest.transform);
            InitializeItem init = newPickup.GetComponent<InitializeItem>();

            init.item = Instantiate(Items[Random.Range(0, Items.Length)]);
            init.item.Quantity = Random.Range(2, 5);

            StartCoroutine(ApplyForce(newPickup));
        }
    }

    private IEnumerator ApplyForce(GameObject obj)
    {
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
}
