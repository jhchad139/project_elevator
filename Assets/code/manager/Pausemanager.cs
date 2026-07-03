using UnityEngine;
using UnityEngine.InputSystem;

public class Pausemanager : MonoBehaviour
{
    public bool isPause = false;

    public GameObject pauseCanvas;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPause = !isPause;
        
        pauseCanvas.SetActive(isPause);

        Time.timeScale = isPause ? 0.0f : 1.0f;
        
    }
}
