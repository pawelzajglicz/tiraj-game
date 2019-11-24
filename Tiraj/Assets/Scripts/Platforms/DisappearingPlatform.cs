using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float beforeDisappearingTime = 1.5f;
    public float disapperingTime = 1.5f;
    public float nonDisapperingableTime = 1f;
    
    public float currentDisapperingTime = 0f;
    public float currentNonDisapperingableTime = 0f;

    private bool isInteractable = true;

    private BoxCollider2D boxCollider;
    private SpriteRenderer sprtiteRenderer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprtiteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isInteractable) return;

        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            ProcessDisappearingSequence();
        }
    }

    private void ProcessDisappearingSequence()
    {
        isInteractable = false;
        StartCoroutine(ProcessDisappearing());
    }

    IEnumerator ProcessDisappearing()
    {
        yield return new WaitForSeconds(beforeDisappearingTime);
        boxCollider.enabled = false;
        sprtiteRenderer.enabled = false;

        StartCoroutine(ProcessAppearing());
    }

    IEnumerator ProcessAppearing()
    {
        yield return new WaitForSeconds(disapperingTime);
        boxCollider.enabled = true;
        sprtiteRenderer.enabled = true;

        StartCoroutine(ProcessInteractability());
    }

    IEnumerator ProcessInteractability()
    {
        yield return new WaitForSeconds(nonDisapperingableTime);

        isInteractable = true;
    }
}
