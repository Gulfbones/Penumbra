using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern_Checker : MonoBehaviour
{
    public lanternInteract[] lanterns;
    public Light_Puzzle_Checker puzzle;
    public bool solved;
    public float timer;

    void Start()
    {
        lanterns = gameObject.GetComponentsInChildren<lanternInteract>();
        puzzle = gameObject.GetComponent<Light_Puzzle_Checker>();
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    bool GetSolved()
    {
        return solved;
    }
}
