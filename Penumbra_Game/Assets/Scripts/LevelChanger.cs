using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (ChangeScene.gameObject.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        Debug.Log("Go to level: " + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    //public void StartLevel()
    //{
    //    SceneManager.LoadScene("Sprint_3_03");
    //}
    public void RestartLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);//"Sprint_3_03");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}