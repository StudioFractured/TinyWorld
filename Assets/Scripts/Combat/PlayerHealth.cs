using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Cinemachine;

public class PlayerHealth : HealthBehaviour
{
    public Transform gridParent;
    public GameObject hpPrefab;

    private int lastValue;
    private List<GameObject> hpObjects = new List<GameObject>();

    public CinemachineCamera virtualCam;
    private CinemachinePositionComposer positionComposer;

    public float duration;
    public float intensity;

    private void Start()
    {
        lastValue = Mathf.FloorToInt(_currentValue);

        for (int i = 0; i < lastValue; i++)
        {
            GameObject hp = Instantiate(hpPrefab, gridParent);
            hpObjects.Add(hp);
        }

        if (virtualCam != null)
        {
            positionComposer = virtualCam.GetComponent<CinemachinePositionComposer>();
        }
    }

    private void Update()
    {
        int currentValueInt = Mathf.FloorToInt(_currentValue);

        if (currentValueInt > lastValue)
        {
            int difference = currentValueInt - lastValue;

            for (int i = 0; i < difference; i++)
            {
                GameObject hp = Instantiate(hpPrefab, gridParent);
                hpObjects.Add(hp);
            }
        }
        else if (currentValueInt < lastValue)
        {
            int difference = lastValue - currentValueInt;

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

        lastValue = currentValueInt;
    }

    public void ShakeCam()
    {
        if (positionComposer != null)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalOffset = positionComposer.TargetOffset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            positionComposer.TargetOffset = new Vector3(offsetX, offsetY, originalOffset.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        positionComposer.TargetOffset = originalOffset;
    }
}
