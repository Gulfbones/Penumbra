using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_UI : MonoBehaviour
{
    public PlayerScript playerScript;
    public Vector3 initialPos;
    public float maxWax;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>(); // Gets the PlayerScript
        maxWax = playerScript.getWaxMax();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves top of candle down based on Current wax / Max wax
        transform.localPosition = new Vector3(initialPos.x,initialPos.y - (6.5f - (6.5f * (playerScript.getWaxCurrent()/maxWax))), initialPos.x);
        if (playerScript.getWaxCurrent() <= 0)
        {
            Debug.LogWarning("You Lose!");
            transform.parent.gameObject.SetActive(false);
            //Destroy(transform.parent.gameObject);
        }
    }
}
