using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorial, text;
    private GameObject canvas;
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            tutorial.SetActive(false);
            text.SetActive(false);
            Time.timeScale = 1;
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            canvas.transform.GetChild(5).gameObject.SetActive(true);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && count == 0)
        {

            Time.timeScale = 0f;
            tutorial.SetActive(true);
            text.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;

        }

        if (other.gameObject.CompareTag("Player") && count == 1)
        {

            Time.timeScale = 0f;
            tutorial.SetActive(true);
            tutorial.transform.GetChild(0).gameObject.SetActive(false);
            text.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;

        }
        if (other.gameObject.CompareTag("Player") && count == 2)
        {

            Time.timeScale = 0f;
            tutorial.SetActive(true);
            tutorial.transform.GetChild(0).gameObject.SetActive(false);
            tutorial.transform.GetChild(3).gameObject.SetActive(false);
            tutorial.transform.GetChild(1).gameObject.SetActive(false);
            text.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;

        }
        if (other.gameObject.CompareTag("Player") && count == 3)
        {

            Time.timeScale = 0f;
            tutorial.SetActive(true);
            tutorial.transform.GetChild(0).gameObject.SetActive(false);
            tutorial.transform.GetChild(2).gameObject.SetActive(false);
            tutorial.transform.GetChild(1).gameObject.SetActive(false);
            text.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;

        }
        if (other.gameObject.CompareTag("Player") && count == 4)
        {
            Time.timeScale = 0f;
            tutorial.SetActive(true);
            tutorial.transform.GetChild(0).gameObject.SetActive(false);
            tutorial.transform.GetChild(2).gameObject.SetActive(false);
            tutorial.transform.GetChild(1).gameObject.SetActive(false);
            tutorial.transform.GetChild(4).gameObject.SetActive(false);
            text.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;
        }
    }
}

