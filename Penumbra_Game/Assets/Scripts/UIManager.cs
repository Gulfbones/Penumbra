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

        isPaused = false;
        Time.timeScale = 1;
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

            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);


            Time.timeScale = 0f;
        }

        else
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);

            canvas.transform.GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            canvas.transform.GetChild(5).gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }


}
