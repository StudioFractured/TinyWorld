using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [Header("Time Settings")]
    [Tooltip("Game time multiplier (e.g., 1 = normal speed, 0.5 = half speed, 2 = double speed)")]
    public float gameTimeScale = 1.0f;

    void Update()
    {
        Time.timeScale = gameTimeScale;
    }
}
