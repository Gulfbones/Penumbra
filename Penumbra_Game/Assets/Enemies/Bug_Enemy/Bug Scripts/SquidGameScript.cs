using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidGameScript : MonoBehaviour
{
    public Vector3 startPosition;
    public GameObject light;
    public PolygonCollider2D lightCollider;
    public BoxCollider2D enemyCollider;
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
        lightCollider = light.GetComponent<PolygonCollider2D>();
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
                lightCollider.enabled = false;
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
