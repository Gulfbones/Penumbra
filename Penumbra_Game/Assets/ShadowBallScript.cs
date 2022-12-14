using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBallScript : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float waitInterval;
    public Vector3 startPosition;

    public bool isActive;


    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        startPosition = transform.position;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (waitInterval <= 0)
        {
            anim.transform.position = Vector2.MoveTowards(anim.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            waitInterval -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
            transform.position = startPosition;
            isActive = false;
        }
        if(obj.gameObject.CompareTag("Light"))
        {
            if(player.GetComponent<PlayerScript>().attacking == true)
            {
                transform.position = startPosition;
                isActive = false;
            }
        }
    }

}
