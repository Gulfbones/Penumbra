using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public Vector3 rightMovement;
    public Vector3 leftMovement;
    public Vector3 upMovement;
    public Vector3 downMovement;
    public bool moving;
    public float animTriggerVelocity = 9.5f; // How fast you need to move to trigger walk animation
    public Rigidbody2D playerPhysicsEngine;

    public GameObject down;
    public GameObject up;
    public GameObject left;
    public GameObject right;
    public GameObject currentDirection;

    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer PlayerCandleSpriteRenderer;

    public GameObject OriginalGameObject;
    public GameObject playerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start ran");

        // not GetComponent<GameObject>(); b/c itself is not a game object
        OriginalGameObject = gameObject;

        // Getting sprite Animation and rendering for player
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

        // Getting physics engine
        playerPhysicsEngine = GetComponent<Rigidbody2D>();

        // Gets the first child gameObject of the main player 
        playerObject = OriginalGameObject.transform.GetChild(0).gameObject;
         
        //Gets the the SpriteRenderer of the first childs, first child of the main player
        PlayerCandleSpriteRenderer = playerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        down = OriginalGameObject.transform.GetChild(1).gameObject;
        up = OriginalGameObject.transform.GetChild(2).gameObject;
        left = OriginalGameObject.transform.GetChild(3).gameObject;
        right = OriginalGameObject.transform.GetChild(4).gameObject;
        currentDirection = right;

        rightMovement = new Vector3(10, 0, 0);  // x, y, z
        leftMovement = new Vector3(-10, 0, 0);  // x, y, z
        upMovement = new Vector3(0, 10, 0);     // x, y, z
        downMovement = new Vector3(0, -10, 0);  // x, y, z

        moving = false;
        // Probably not needed \/
        //Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("MinecartWall").GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }
    // Clears all the player game objects
    void ClearActive(string direction)
    {
        if(direction != "up")
            up.SetActive(false);
        if (direction != "down")
            down.SetActive(false);
        if (direction != "left")
            left.SetActive(false);
        if (direction != "right")
            right.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        // Can only move LEFT or RIGHT
        if (Input.GetKey(KeyCode.D)) // RIGHT
        {
            if (moving == false)
            {
                moving = true;
                currentDirection = down;
                right.SetActive(true);
                ClearActive("right");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Sets the Rotation of the child object "Player Object" which also rotates all other children objects
            }
            playerPhysicsEngine.velocity = new Vector3(rightMovement.x, playerPhysicsEngine.velocity.y, 0); // Adds Velocity which causes movement

        }
        else if (Input.GetKey(KeyCode.A)) // LEFT
        {
            if (moving == false)
            {
                moving = true;
                currentDirection = down;
                left.SetActive(true);
                ClearActive("left");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, 180); 
            }
            playerPhysicsEngine.velocity = new Vector3(leftMovement.x, playerPhysicsEngine.velocity.y, 0);


        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            moving = false;
        }
        // Can only move UP or DOWN
        if (Input.GetKey(KeyCode.W)) // UP
        {
            if (moving == false)
            {
                moving = true;
                currentDirection = down;
                up.SetActive(true);
                ClearActive("up");
            playerObject.transform.rotation = Quaternion.Euler(0, 0, 90); 
            }
                playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, upMovement.y, 0);



        }
        else if (Input.GetKey(KeyCode.S)) // DOWN
        {
            if (moving == false)
            {
                moving = true;
                currentDirection = down;
                down.SetActive(true);
                ClearActive("down");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, downMovement.y, 0);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moving = false;
        }
        // if velocity > animTriggerSpeed, play animation
        if (down.gameObject.activeSelf)
        {
            if (playerPhysicsEngine.velocity.x > animTriggerVelocity || playerPhysicsEngine.velocity.y > animTriggerVelocity
                || playerPhysicsEngine.velocity.x < -animTriggerVelocity || playerPhysicsEngine.velocity.y < -animTriggerVelocity)
            {
                //currentDirection.GetComponent<Animator>().SetBool("playWalk",true);
               // Debug.Log("playing walk");
                down.GetComponent<Animator>().SetBool("playWalk", true);
            }
            else down.GetComponent<Animator>().SetBool("playWalk", false);
            //else currentDirection.GetComponent<Animator>().SetBool("playWalk",false); 
        }
        //move
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "MinecartWall")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
}
