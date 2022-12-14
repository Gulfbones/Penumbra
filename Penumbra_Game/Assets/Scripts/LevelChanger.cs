using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
    [SerializeField] private GameObject creditScreen;
    public PlayerScript pcScript;
    public bool goNext;

    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (ChangeScene.gameObject.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        StartCoroutine(RestartCoroutine());
        
    }

    //public void StartLevel()
    //{
    //    SceneManager.LoadScene("Sprint_3_03");
    //}
    public void RestartLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);//"Sprint_3_03");
        pcScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        pcScript.setWaxCurrent(pcScript.getWaxMax());
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditScreen.SetActive(true);
    }

    public void Return()
    {
        creditScreen.SetActive(false);   
    }

    public IEnumerator RestartCoroutine()
    {
        
        GameObject.Find("FadeSquare").gameObject.GetComponent<Fading>().fadeIn(0.8f);
        Debug.Log("Go to level: " + _nextLevelName);
        pcScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>().enabled = false;
        //pcScript = 
        //GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<>.SetActive(false);
        pcScript.setWaxCurrent(pcScript.getWaxMax());
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_nextLevelName);
    }

}