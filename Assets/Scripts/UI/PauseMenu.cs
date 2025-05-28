using UnityEngine;

public class PauseMenu : CanvasView
{
    [Header("// READONLY")]
    [SerializeField] bool _isPaused = false;

    private bool isMuted = false;
    public GameObject AudioOn;
    public GameObject AudioOff;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;
            Show();
        }
        else
        {
            Time.timeScale = 1;
            Hide();
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
