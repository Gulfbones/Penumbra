using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHordeMovementR : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public float speed;

    private int rand;

    void Awake()
    {
        box1 = GameObject.Find("Box1R");
        box2 = GameObject.Find("Box2R");
        box3 = GameObject.Find("Box3R");
    }
    // Start is called before the first frame update
    void Start()
    {
        rand = 0;
        rand = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (rand == 0)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, box1.transform.position, speed * Time.deltaTime);
        }
        else if (rand == 1)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, box2.transform.position, speed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, box3.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("DespawnBoxR"))
        {
            Debug.Log("Collided with DespawnBox");
            Destroy(this.gameObject);
        }
    }
}
