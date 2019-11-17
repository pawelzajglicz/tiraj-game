using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FlyingEnemy : MonoBehaviour
{
    
    public float boost = 500;
    public float boostInterval = 3f;
    public float timeFromLastBoost;
    public bool isMoving;

    public Rigidbody2D rigidBody;
    private Animator animator;

    [SerializeField] GameObject plopVFX;
    private float deathTime = 1.5f;
    private bool isPaused;
    private Vector2 memberedVelocity;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    internal void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(plopVFX, transform.position, Quaternion.identity);
        Destroy(explosion, deathTime);
    }

    void Update()
    {
        ManageBoost();
    }

    internal void StartFly()
    {
        isMoving = true;
    }

    private void ManageBoost()
    {
        if (!isMoving) return;


        timeFromLastBoost += Time.deltaTime;

        if (timeFromLastBoost > boostInterval)
        {
            timeFromLastBoost = 0;
            Boost();
        }
    }

    private void Boost()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {

            Vector2 heading = player.transform.position - transform.position;
            Vector2 direction = heading.normalized;

            rigidBody.AddForce(direction * boost);
            animator.SetTrigger("boost");
            ServeFacing(direction);
        }
    }

    private void ServeFacing(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
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
