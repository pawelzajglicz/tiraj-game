using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    private Animator animator;
    public bool isReady;
    public float springForce = 700;
    private float timeForRecharge = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        isReady = true;
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            Release(colliderGameObject);
        }
    }

    private void Release(GameObject colliderGameObject)
    {
        isReady = false;

        Rigidbody2D rigidBody = colliderGameObject.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.AddForce(new Vector2(0, springForce));
            animator.SetTrigger("release");
        }

        StartCoroutine(ProcessReleasing());
    }

    private IEnumerator ProcessReleasing()
    {
        yield return new WaitForSeconds(timeForRecharge);
        animator.SetTrigger("beReady");
        isReady = true;
    }
}
