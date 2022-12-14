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
    public GameObject plant3;


    void Start()
    {
        lanterns = gameObject.GetComponentsInChildren<lanternInteract>();
        puzzle = GameObject.Find("Lantern_Puzzle_Checker").GetComponent<Light_Puzzle_Checker>();
        solved = false;
        numLit = 0;
        
        //rat1 = GameObject.FindGameObjectWithTag("rat1");
        //rat1.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //UnityEngine.Debug.Log("numLit: " + numLit);
        //enableEnemies();
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


    bool GetSolved()
    {
        return solved;
    }
}
