using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 500;
    public float jumpForce = 325;

    private float movementFactor = 0f;
    private bool movingEnabled = false;
    private float xDisplacement;
    public bool isGrounded = false;
    private float xVelocity;
    private float xVelocityAbs;
    public bool isPaused = false;
    public Vector2 memberedVelocity;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private static int horizontalDirection = 1;


    void Start()
    {
        animator = GetComponent<Animator>();
        isGrounded = false;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isPaused)
        {
            ServeJumping();
            ServeAnimations();
            ServeFacingDirection();
            ServeHorizontalMoving();
        }
    }


    private void ServeHorizontalMoving()
    {
        if (!movingEnabled) return;

        xDisplacement = Input.GetAxis("Horizontal") * speed * Time.deltaTime * horizontalDirection;
        rigidBody.velocity = new Vector2(xDisplacement, rigidBody.velocity.y);
    }

    private void ServeJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
    }

    private void ServeAnimations()
    {
        animator.SetFloat("runSpeed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("jumpSpeed", rigidBody.velocity.y);
    }

    private void ServeFacingDirection()
    {
        xVelocity = rigidBody.velocity.x;
        xVelocityAbs = Mathf.Abs(xVelocity); // necessary to fix occasionally bug with reversing facing every frame

        if (xVelocity < 0 && xVelocityAbs > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (xVelocity > 0 && xVelocityAbs > 0.01)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        animator.SetTrigger("grounded");
        movingEnabled = true;
    }

    public static void ReverseHorizontal()
    {
        horizontalDirection *= -1;
    }

    public void Resume()
    {
        isPaused = false;
        animator.enabled = true;
        rigidBody.isKinematic = false;
        rigidBody.WakeUp();
        rigidBody.velocity = memberedVelocity;
    }

    public void Pause()
    {
        isPaused = true;
        animator.enabled = false;
        memberedVelocity = rigidBody.velocity;
        rigidBody.isKinematic = true;
        rigidBody.Sleep();
    }

}
