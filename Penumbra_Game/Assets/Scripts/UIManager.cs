using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, canvas;

    public static bool isPaused;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }
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
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        else
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
