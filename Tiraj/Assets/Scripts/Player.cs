﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 500;
    public float jumpForce = 325;
    private bool isGrounded;

    private float xDisplacement;

    private Rigidbody2D rigidBody;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isGrounded = false;
    }


    void Update()
    {
        xDisplacement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rigidBody.velocity = new Vector2(xDisplacement, rigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }

        animator.SetFloat("runSpeed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("jumpSpeed", rigidBody.velocity.y);

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
    }
}