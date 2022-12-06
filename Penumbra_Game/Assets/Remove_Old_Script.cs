using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Old_Script : MonoBehaviour
{
    [SerializeField] GameObject enemySleepingGroup;
    [SerializeField] GameObject door;
    //[SerializeField] private Button_Door door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(enemySleepingGroup);
            door.SetActive(true);
            Destroy(this);
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
