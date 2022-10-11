using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_UI : MonoBehaviour
{
    public pcScript playerScript;
    public Vector3 initialPos;
    public float maxWax;
    // Start is called before the first frame update
    void Start()
    {
        //top = gameObject;
        initialPos = transform.localPosition;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<pcScript>();
        maxWax = playerScript.getWaxMax();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(initialPos.x,initialPos.y - (6.5f - (6.5f * (playerScript.getWaxCurrent()/maxWax))), initialPos.x);
        if (playerScript.getWaxCurrent() <= 0)
        {
            Debug.LogWarning("You Lose!");
            Destroy(transform.parent.gameObject);
            //UnityEngine.Debug.Log("UI destroyed");//testing
        }
    }
}
