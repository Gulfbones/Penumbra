using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorial, canvas;
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
            Time.timeScale = 1.0f;
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
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(3).gameObject.SetActive(false);
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            count++;

        }
    }
}

