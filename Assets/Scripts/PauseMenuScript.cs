using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
    public GameObject PausedUI;

    private bool paused = false;
    private bool lockPauseUI = false;
    private bool victory = false;

    void Start()
    {
        PausedUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused && !lockPauseUI)
        {
            PausedUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PausedUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // may or may not need this
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Resume()
    {
        paused = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void World()
    {
        SceneManager.LoadScene("World Map");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
