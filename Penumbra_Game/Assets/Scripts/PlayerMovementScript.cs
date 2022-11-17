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
    //public MeshRenderer currentDirection;

    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer PlayerCandleSpriteRenderer;

    public GameObject OriginalGameObject;
    public GameObject playerObject;
    private int wasFacing;
    
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


        down = OriginalGameObject.transform.GetChild(1).gameObject;//.GetComponent<MeshRenderer>();
        up = OriginalGameObject.transform.GetChild(2).gameObject;//.GetComponent<MeshRenderer>();
        left = OriginalGameObject.transform.GetChild(3).gameObject;//.GetComponent<MeshRenderer>();
        right = OriginalGameObject.transform.GetChild(4).gameObject;//.GetComponent<MeshRenderer>();
        //currentDirection = right;

        rightMovement = new Vector3(10, 0, 0);  // x, y, z
        leftMovement = new Vector3(-10, 0, 0);  // x, y, z
        upMovement = new Vector3(0, 10, 0);     // x, y, z
        downMovement = new Vector3(0, -10, 0);  // x, y, z

        moving = false;
        // Probably not needed \/
        //Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("MinecartWall").GetComponent<Collider2D>(), GetComponent<Collider2D>());

        wasFacing = 0;
        //using 0=up, 1=right, 2=down, 3=left

    }
    // Clears all the player game objects
    void ClearActive(string direction)
    {
        if(direction != "up")
            foreach(var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
            //up.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "down")
            foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //down.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "left")
            foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //left.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "right")
            foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //right.GetComponentsInChildren<Renderer>().enabled = (false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
        // Can only move LEFT or RIGHT
        if (Input.GetKey(KeyCode.D)) // RIGHT
        {
            slowTurn();
                moving = true;
            //currentDirection = right;
            //right.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("right");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Sets the Rotation of the child object "Player Object" which also rotates all other children objects
            
            playerPhysicsEngine.velocity = new Vector3(rightMovement.x, playerPhysicsEngine.velocity.y, 0); // Adds Velocity which causes movement
            wasFacing = 1;
        }
        else if (Input.GetKey(KeyCode.A)) // LEFT
        {
            slowTurn();
                moving = true;
            //currentDirection = left;
            //left.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("left");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, 180); 
            
            playerPhysicsEngine.velocity = new Vector3(leftMovement.x, playerPhysicsEngine.velocity.y, 0);

            wasFacing = 3;
        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            moving = false;
        }
        // Can only move UP or DOWN
        if (Input.GetKey(KeyCode.W)) // UP
        {
            slowTurn();
                moving = true;
            //currentDirection = up;
            //up.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("up");
            playerObject.transform.rotation = Quaternion.Euler(0, 0, 90); 
            
                playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, upMovement.y, 0);


            wasFacing = 0;
        }
        else if (Input.GetKey(KeyCode.S)) // DOWN
        {
            slowTurn();
                moving = true;
            //currentDirection = down;
            //down.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("down");
                playerObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            
            playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, downMovement.y, 0);
            wasFacing = 2;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moving = false;
        }
        // if velocity > animTriggerSpeed, play animation
        
        if (playerPhysicsEngine.velocity.x > animTriggerVelocity || playerPhysicsEngine.velocity.y > animTriggerVelocity
            || playerPhysicsEngine.velocity.x < -animTriggerVelocity || playerPhysicsEngine.velocity.y < -animTriggerVelocity)
        {
            up.GetComponent<Animator>().SetBool("playWalk", true);
            down.GetComponent<Animator>().SetBool("playWalk", true);
            left.GetComponent<Animator>().SetBool("playWalk", true);
            right.GetComponent<Animator>().SetBool("playWalk", true);
            // Debug.Log("playing walk");
            //down.GetComponent<Animator>().SetBool("playWalk", true);
        }
        //else down.GetComponent<Animator>().SetBool("playWalk", false);
        else
        {
            up.GetComponent<Animator>().SetBool("playWalk", false);
            down.GetComponent<Animator>().SetBool("playWalk", false);
            left.GetComponent<Animator>().SetBool("playWalk", false);
            right.GetComponent<Animator>().SetBool("playWalk", false);
        }
        
        
    }

    private void slowTurn()
    {
        if (Input.GetKey(KeyCode.W)) //if holding up input
        {
            if (wasFacing == 1) //if they were facing right
            {
                gameObject.transform.eulerAngles = new Vector3(0, 40, 0);
                UnityEngine.Debug.Log("whoo slow turn baby!");
            }
            else if (wasFacing == 2) //if they were facing down
            {

            }
            else if (wasFacing == 3) //if they were facing left
            {

            }
            else
            {

            }
        }
        else if (Input.GetKey(KeyCode.D)) //if holding right input
        {
            if (wasFacing == 0) //if they were facing up
            {

            }
            else if (wasFacing == 2) //if they were facing down
            {

            }
            else if (wasFacing == 3) //if they were facing left
            {

            }
            else
            {

            }
        }
        else if (Input.GetKey(KeyCode.S)) //if holding down input
        {
            if (wasFacing == 0) //if they were facing up
            {

            }
            else if (wasFacing == 1) //if they were facing right
            {

            }
            else if (wasFacing == 3) //if they were facing left
            {

            }
            else
            {

            }
        }
        else if (Input.GetKey(KeyCode.A)) //if holding left input
        {
            if (wasFacing == 0) //if they were facing up
            {

            }
            else if (wasFacing == 1) //if they were facing right
            {

            }
            else if (wasFacing == 2) //if they were facing down
            {

            }
            else
            {

            }
        }
        else
        {


        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "MinecartWall")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
}
