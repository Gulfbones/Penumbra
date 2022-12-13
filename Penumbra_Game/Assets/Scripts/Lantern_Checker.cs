using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern_Checker : MonoBehaviour
{
    public lanternInteract[] lanterns;
    public Light_Puzzle_Checker puzzle;
    public bool solved;
    public float timer;
    public int numLit;
    public GameObject plant1;

    void Start()
    {
        lanterns = gameObject.GetComponentsInChildren<lanternInteract>();
        puzzle = GameObject.Find("Light_Puzzle_Checker").gameObject.GetComponent<Light_Puzzle_Checker>();
        solved = false;
        numLit = 0;
        plant1 = GameObject.FindGameObjectWithTag("plant1");
        plant1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lanterns.Length; i++)
        {
            if (lanterns[i].lit == true)
            {
                numLit++;
            }
        }
        enableEnemies();
        timer += 1.0f * Time.deltaTime;
        if (solved == false && timer > 1.0f)
        {
            //Debug.Log("checked");
            check();
            timer = 0.0f;
        }
    }
    /*private void FixedUpdate()
    { 
        
    }*/

    public void check()
    {

        bool checking = true;
        for (int i = 0; i < lanterns.Length; ++i)
        {
            if (lanterns[i].lit == false && puzzle.getSolved() == true)
            {
                
                checking = false;
            }
        }
        if (checking == true)
        {
            Destroy(GameObject.Find("ShadowWall"));
        }
    }

    public void enableEnemies()
    {
        if (numLit > 3)
        {
            //enable enemy 1
            plant1.SetActive(true);
        }
        if (numLit > 5)
        {
            //enable enemy 2
        }
    }

    bool GetSolved()
    {
        return solved;
    }
}
