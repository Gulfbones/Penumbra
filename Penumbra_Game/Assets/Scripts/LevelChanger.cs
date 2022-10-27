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

    void GoToNextLevel()
    {
        Debug.Log("Go to level: " + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }
}