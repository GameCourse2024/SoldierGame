using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMover : MonoBehaviour
{
    private Animator myAnimation;   // animation

    [SerializeField] private GameObject knifePrefab;    // setting the prefab
    [SerializeField] Transform throwPos;    // kunai throwing position
    [SerializeField] float cooldownShoot = 0.5f; // cooldown between shots;
    private float lastShootTime = -1; // makes the player shoot at the start without cooldown

    //-------------------------------------------------------------------------------------------/
    Rigidbody2D rigitBod;   // accessing components of the player

    [SerializeField]
    private float speed = 1f;      // default player speed
    public bool facingRight;  // facing right or left
    [SerializeField] int jumpPower; // player jump power

    // all the directions the player can move
    [Tooltip("Button to move right")]
    [SerializeField]
    private KeyCode rightMove;      
    [Tooltip("Button to move left")]
    [SerializeField]
    private KeyCode leftMove;

    [Tooltip("Button to move up")]
    [SerializeField]
    private KeyCode upMove;

    [Tooltip("Button to throw knife")]
    [SerializeField]
    private KeyCode throwKnife;

    private bool isJumping = false;


    private void Start() 
    {
        rigitBod = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();

        facingRight = true;
        // simple guidance about the keys
        Debug.Log("These are the buttons: " + upMove +"," + leftMove +"," +rightMove +"," +throwKnife);    
    }
    void FixedUpdate() 
    {
        // up or down direction
        float horizontalMovement = 0;
        float verticalMovement = 0;

        // moves according to the key pressed
        if (Input.GetKey(rightMove))    // right
        {
            horizontalMovement = speed;
        }
        else if (Input.GetKey(leftMove))    // left
        {
            horizontalMovement = -speed;
        }

        if (!isJumping && Input.GetKey(upMove))       // jump
        {
            rigitBod.velocity = new Vector2(rigitBod.velocity.x, jumpPower);
        }

        if (Input.GetKey(throwKnife))
        {
            ThrowKnife();
        }

        myAnimation.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        // modifies the character location based on the key pressed
        // deltaTime - time in seconds it took from last frame to current
        // we change the x,y coordinate no need to edit z
        transform.position += new Vector3(horizontalMovement * Time.deltaTime, verticalMovement * Time.deltaTime, 0);

        Flip(horizontalMovement);   // flips the char when it moves right/left
    }

    private void Flip(float flip)
    {
        // flips the char when facing right/left by x scale 
        if (flip > 0 && !facingRight || flip < 0 && facingRight )
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void ThrowKnife()
    {
        // cooldown between each attack
        if(Time.time - lastShootTime >= cooldownShoot || lastShootTime < 0f)
        {
            // if facing right then attack to right side           
            if(facingRight)
            {   
                GameObject throwRight = (GameObject)Instantiate(knifePrefab, throwPos.position, Quaternion.Euler(new Vector3(0,0,-90)));
                throwRight.GetComponent<Knife>().GetDirection(Vector2.right);
            }
            else
            {
                GameObject throwLeft = (GameObject)Instantiate(knifePrefab, throwPos.position, Quaternion.Euler(new Vector3(0,0,90)));
                throwLeft.GetComponent<Knife>().GetDirection(Vector2.left);
            }

            // indicate the last shoot time to start the cooldown
            lastShootTime = Time.time;
        }
        else return;
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "Platform")
    //     {
    //         isJumping = false;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "Platform")
    //     {
    //         isJumping = true;
    //     }
    // }

}