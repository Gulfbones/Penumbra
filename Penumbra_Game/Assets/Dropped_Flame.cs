using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropped_Flame : MonoBehaviour
{
    float lifeTimer;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= 1.0f * Time.deltaTime;
        if (lifeTimer <= 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0, 0, 0), 1.0f * Time.deltaTime);
            if(lifeTimer <= -1.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
