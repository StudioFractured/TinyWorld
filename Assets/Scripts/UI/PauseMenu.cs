using UnityEngine;

public class PauseMenu : CanvasView
{
    [Header("// READONLY")]
    [SerializeField] bool _isPaused = false;

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
}
