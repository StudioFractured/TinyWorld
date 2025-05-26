using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    private bool isPaused = false;
    private bool isMuted = false;
    public GameObject AudioOn;
    public GameObject AudioOff;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            UI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            UI.SetActive(false);
        }
    }

    public void ToggleAudio()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            AudioListener.volume = 0f;
            AudioOff.SetActive(true);
            AudioOn.SetActive(false);
        }
        else
        {
            AudioListener.volume = 1.0f;
            AudioOff.SetActive(false);
            AudioOn.SetActive(true);
        }
    }
}
