using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_RatL : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public float speed;

    private Transform box1Loc;
    private Transform box2Loc;
    private Transform box3Loc;
    private Animator anim;
    private int rand;

    void Awake()
    {
        box1 = GameObject.Find("Box1L");
        box2 = GameObject.Find("Box2L");
        box3 = GameObject.Find("Box3L");
    }
    // Start is called before the first frame update
    void Start()
    {
        box1Loc = box1.transform;
        box2Loc = box2.transform;
        box3Loc = box3.transform;
        anim = GetComponent<Animator>();

        rand = 0;
        rand = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (rand == 0)
        {
            anim.transform.position = Vector2.MoveTowards(anim.transform.position, box1Loc.position, speed * Time.deltaTime);
        }
        else if (rand == 1)
        {
            anim.transform.position = Vector2.MoveTowards(anim.transform.position, box2Loc.position, speed * Time.deltaTime);
        }
        else
        {
            anim.transform.position = Vector2.MoveTowards(anim.transform.position, box3Loc.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("DespawnBoxL"))
        {
            Debug.Log("Collided with DespawnBox");
            Destroy(this.gameObject);
        }
    }
}
