using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject win;
    public GameObject lose;

    public GameObject PauseMenuUi;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !win.activeSelf && !lose.activeSelf)
        {
            if (GameIsPaused)
            {
                Resume();
                TimerController.instance.Resume();
            }
            else
            {

                TimerController.instance.Pause();
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
