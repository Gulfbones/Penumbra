using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    private float turnAmount = 5.0f;
    //private float fullTurnAmount;
    public bool handleCollected;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;
    public GameObject rails;
    // Start is called before the first frame update
    void Start()
    {
        turnAmount = 0.0f;
        //fullTurnAmount = 100.0f;
        handleCollected = false;
        rails = GameObject.Find("Tilemap_Moving_Rails");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollectHandle()
    {
        handleCollected = true;
        spriteRenderer.sprite = newSprite;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (handleCollected && other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            //Debug.Log("DOING");
            rails.transform.localPosition = Vector3.MoveTowards(rails.transform.localPosition, new Vector3(6,0,0), 1.0f * Time.deltaTime); ;
        }
    }

}
