using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_Script : MonoBehaviour
{

    public SpriteRenderer sprite;
    public GameObject mainGameObject;
    //public GameObject cart;
    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite sideSprite;
    private Rigidbody2D CartPhysicsEngine;
    //private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mainGameObject = gameObject;
        sprite = GetComponent<SpriteRenderer>();
        //cart = mainGameObject.transform.GetChild(0).gameObject;
        CartPhysicsEngine = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //gameObject.get
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CartPhysicsEngine.velocity.x = " + CartPhysicsEngine.velocity.x);
        //Debug.Log("CartPhysicsEngine.velocity.y = " + CartPhysicsEngine.velocity.y);
        if (CartPhysicsEngine.velocity.x > 1.0f || CartPhysicsEngine.velocity.x < -1.0f)
        {
            //CartPhysicsEngine.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            sprite.sprite = sideSprite;
        }
        if (CartPhysicsEngine.velocity.y > 1.0f || CartPhysicsEngine.velocity.y < -1.0f)
        {
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            sprite.sprite = upSprite;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Exit Rock")
        {
            Debug.LogWarning("YOU WIN!!!!");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "MineCartPusher")
        {
            CartPhysicsEngine.velocity = new Vector3(CartPhysicsEngine.velocity.y, CartPhysicsEngine.velocity.x, 0);
            //Physics2D.IgnoreCollision(GameObject.tag("Player").collider, GetComponent<Collider2D>());
        }
    }
}