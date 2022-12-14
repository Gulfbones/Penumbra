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
    //public GameObject rat1;
    public GameObject plant2;

    void Start()
    {
        lanterns = gameObject.GetComponentsInChildren<lanternInteract>();
        puzzle = GameObject.Find("Lantern_Puzzle_Checker").GetComponent<Light_Puzzle_Checker>();
        solved = false;
        numLit = 0;
        plant1 = GameObject.FindGameObjectWithTag("plant1");
        plant1.SetActive(false);
        //rat1 = GameObject.FindGameObjectWithTag("rat1");
        //rat1.SetActive(false);
        plant2 = GameObject.FindGameObjectWithTag("plant2");
        plant2.SetActive(false);
        for (int i = 0; i < lanterns.Length; i++)
        {
            if (lanterns[i].lit == true)
            {
                numLit++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        numLit = 0;
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
            if (lanterns[i].lit == false && puzzle.getSolved() == false)
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
        /*if (numLit > 9)
        {
            rat1.SetActive(true);
        }*/
        if (numLit > 10)
        {
            plant2.SetActive(true);
        }
        if (numLit > 12)
        {
            plant1.SetActive(true);
        }

    }

    bool GetSolved()
    {
        return solved;
    }
}
