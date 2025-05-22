using UnityEngine;

public class RemoveParent : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
    }
}
