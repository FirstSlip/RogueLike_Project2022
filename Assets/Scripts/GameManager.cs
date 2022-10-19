using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject statsButton;
    public GameObject optionButton;
    public GameObject inventoryButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionButton.GetComponent<Button>().onClick.Invoke();
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }    
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryButton.GetComponent<Button>().onClick.Invoke();
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            statsButton.GetComponent<Button>().onClick.Invoke();
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
