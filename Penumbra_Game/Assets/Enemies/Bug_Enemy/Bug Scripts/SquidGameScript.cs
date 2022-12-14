using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidGameScript : MonoBehaviour
{
    public Vector3 startPosition;
    GameObject obj;
    private Animator anim;


    void Awake()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        //obj.GetComponent<PlayerMovementScript>().isMoving = true;
    }

    void Update()
    {
       
    }

    void OnTriggerStay2D(Collider2D object1)
    {
        if (object1.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player In Light");
            if(obj.GetComponent<PlayerMovementScript>().isMoving == true)
            {
                Debug.Log("Player moving. Attack.");
                anim.SetTrigger("found");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player");
            anim.SetTrigger("attacked");
            transform.position = startPosition;
        }
    }
}
