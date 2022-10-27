using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public static bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Pause();
        }
    }

    public void Pause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
