using UnityEngine;
using System.Collections.Generic;

public class PlayerHealth : HealthBehaviour
{
    public Transform gridParent;
    public GameObject hpPrefab;

    private float lastValue;
    private List<GameObject> hpObjects = new List<GameObject>();

    private void Start()
    {
        lastValue = _currentValue;

        for (int i = 0; i < _currentValue; i++)
        {
            GameObject hp = Instantiate(hpPrefab, gridParent);
            hpObjects.Add(hp);
        }
    }

    private void Update()
    {
        if (_currentValue > lastValue)
        {
            float difference = _currentValue - lastValue;

            for (int i = 0; i < difference; i++)
            {
                GameObject hp = Instantiate(hpPrefab, gridParent);
                hpObjects.Add(hp);
            }
        }
        else if (_currentValue < lastValue)
        {
            float difference = lastValue - _currentValue;

            for (int i = 0; i < difference; i++)
            {
                if (hpObjects.Count > 0)
                {
                    GameObject lastHp = hpObjects[hpObjects.Count - 1];
                    hpObjects.RemoveAt(hpObjects.Count - 1);
                    Destroy(lastHp);
                }
            }
        }

        lastValue = _currentValue;
    }
}
