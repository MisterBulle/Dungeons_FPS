using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;

    private GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        pausePanel = canvas.transform.Find("Pause_Panel").gameObject;
        pausePanel.SetActive(false);

        isPaused = false;
        Time.timeScale = 1;
    }

    public void Touche_PauseMenu()
    {
        //je quitte pause
        if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        //j'entre dans pause
        else
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        isPaused = true;
        Touche_PauseMenu();
    }



}
