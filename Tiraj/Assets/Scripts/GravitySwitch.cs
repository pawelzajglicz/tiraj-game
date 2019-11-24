using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public int gravityOffTime = 1;
    public Vector2 startGravity;


    private void Start()
    {
        startGravity = Physics2D.gravity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && GameManagerBase.GetInstance().IsGravitySwitchAllowed())
        {
            ProcessGravitySequence();
        }
    }

    private void ProcessGravitySequence()
    {
        if (GameSessionInfo.GetInstance().GetGravitySwitchesLeft() <= 0) return;

        GameManagerBase.GetInstance().RemoveGravitySwitch();
        StartCoroutine(GravitySequence());
    }

    IEnumerator GravitySequence()
    {
        if (Mathf.Abs(Physics2D.gravity.y) > Mathf.Epsilon)
        {
            Physics2D.gravity = Vector2.zero;
            yield return new WaitForSeconds(gravityOffTime);
            TurnOnGravity();
        }

    }

    private void TurnOnGravity()
    {
        Physics2D.gravity = startGravity;
    }

    private void OnDestroy()
    {
        TurnOnGravity();
    }
}
