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

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        Vector2 heading = player.transform.position - transform.position;
        Vector2 direction = heading.normalized;

        rigidBody.AddForce(direction * boost);
        animator.SetTrigger("boost");
        ServeFacing(direction);
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

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(plopVFX, transform.position, Quaternion.identity);
        Destroy(explosion, deathTime);
    }
}
