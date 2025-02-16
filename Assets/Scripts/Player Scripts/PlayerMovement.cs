using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log("Update is running");
        CheckIfGrounded(); // Ensure the player is not jumping in mid-air
        PlayerJump(); // Make the player jump
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxis("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // Checking if the player is on the ground
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.2f, groundLayer);
       Debug.Log("Ground check :" + isGrounded);

        if (isGrounded && jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);
        }
    }

    // Make the player jump
    void PlayerJump()
    {
        //Debug.Log("calling jump");

        if (isGrounded)
        {
           Debug.Log("Player is on the ground");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump Key Pressed!");
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);

              
            }
        }
    }

    // Function that tells us when the Player touches something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player is in the water");
            FindObjectOfType<GameManager>().InWater();
        }
    }
} // class
