using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 500;
    public float jumpForce = 325;
    public float growingTempo = 0.1f;
    public int childrenLimit = 3;

    private bool isGrowing = false;
    private bool isGrounded;

    private float xDisplacement;
    private float sizeGrowth;
    private Vector2 newSize;
    private float maxSizeFactor = 1.8f;
    private float maxSize;
    private float deathTime = 1.5f;
    private Vector2 startPosition;
    private Vector2 startScale;
    private float movementFactor = 0f;
    private bool movingEnabled = false;
    private int broughtChildren = 0;

    private Rigidbody2D rigidBody;
    private Animator animator;

    [SerializeField] GameObject plopVFX;
    [SerializeField] GameObject playerPrefab;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isGrounded = false;
        maxSize = maxSizeFactor * transform.localScale.x;
        startPosition = transform.position;
        startScale = transform.localScale;
    }


    void Update()
    {
        ServeHorizontalMoving();
        ServeJumping();
        ServeAnimations();
        ServeFacingDirection();
        ServeResizing();
    }

    private void ServeHorizontalMoving()
    {
        if (!movingEnabled) return;

        xDisplacement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rigidBody.velocity = new Vector2(xDisplacement, rigidBody.velocity.y);
    }

    private void ServeResizing()
    {
        if (isGrowing)
        {
            sizeGrowth = growingTempo * Time.deltaTime;
            newSize = new Vector2(transform.localScale.x + sizeGrowth, transform.localScale.y + sizeGrowth);

            transform.localScale = newSize;
        }

        if (transform.localScale.x > maxSize)
        {
            Plop();
        }
    }

    private void Plop()
    {
        Destroy(gameObject);
        BringNewAlien();

        GameObject explosion = Instantiate(plopVFX, transform.position, Quaternion.identity);
        Destroy(explosion, deathTime);
    }

    public void BringNewAlien()
    {
        if (broughtChildren >= childrenLimit) return;

        GameObject newPlayer = Instantiate(playerPrefab, startPosition, Quaternion.identity);
        newPlayer.transform.localScale = startScale;
        newPlayer.GetComponent<Player>().enabled = true;
        newPlayer.GetComponent<BoxCollider2D>().enabled = true;
        newPlayer.GetComponent<Animator>().enabled = true;
        broughtChildren++;
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
        if (rigidBody.velocity.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (rigidBody.velocity.x > 0)
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

    internal void MakeGrowing()
    {
        isGrowing = true;
    }

    internal void StopGrowing()
    {
        isGrowing = false;
    }
}
