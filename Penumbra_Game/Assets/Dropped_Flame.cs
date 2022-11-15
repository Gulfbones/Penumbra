using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropped_Flame : MonoBehaviour
{
    float lifeTimer;
    GameObject trigger;
    Vector3 ogPos;
    Vector3 offSetPos;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 10.0f;
        trigger = gameObject.transform.GetChild(1).gameObject;
        ogPos = trigger.transform.position;
        offSetPos = new Vector3(trigger.transform.position.x, trigger.transform.position.y - 5.0f, trigger.transform.position.z);
        trigger.transform.position = offSetPos;
    }

    // Update is called once per frame
    void Update()
    {

        lifeTimer -= 1.0f * Time.deltaTime;
        if (lifeTimer <= 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0, 0, 0), 1.0f * Time.deltaTime);
            trigger.transform.position = Vector3.MoveTowards(trigger.transform.position, offSetPos, 10.0f * Time.deltaTime);
            if (lifeTimer <= -1.0f)
            {
                Destroy(gameObject);
            }

        }
        if(lifeTimer > 0)
        {
            trigger.transform.position = Vector3.MoveTowards(trigger.transform.position, transform.position, 10.0f * Time.deltaTime);
        }
    }
}
