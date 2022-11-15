using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Puzzle_Checker : MonoBehaviour
{
    // Start is called before the first frame update
    public Puzzle_Lantern[] lanterns;
    public bool solved;
    public float timer;

    void Start()
    {
        lanterns = gameObject.GetComponentsInChildren<Puzzle_Lantern>();
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
        for(int i = 0; i < lanterns.Length; ++i)
        {
            if (lanterns[i].GetLit() == false)
            {
                checking = false;
            }
        }
        if(checking == true)
        {
            //lock all lights and solve
            for (int i = 0; i < lanterns.Length; ++i)
            {
                lanterns[i].LockLights();
            }
            solved = true;
        }
    }
    
    bool GetSolved()
    {
        return solved;
    }
}
