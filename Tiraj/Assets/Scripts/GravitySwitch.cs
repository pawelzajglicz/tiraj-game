﻿using System;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ProcessGravitySequence();
        }
    }

    private void ProcessGravitySequence()
    {
        if (GameManager.GetInstance().switchesLeft <= 0) return;
        GameManager.GetInstance().DecreaseSwitchNumber(1);


        StartCoroutine(GravitySequence());
    }

    IEnumerator GravitySequence()
    {
        if (Mathf.Abs(Physics2D.gravity.y) > Mathf.Epsilon)
        {
            Physics2D.gravity = Vector2.zero;
            yield return new WaitForSeconds(gravityOffTime);
            turnOnGravity();
        }

    }

    private void turnOnGravity()
    {
        Physics2D.gravity = startGravity;
    }
}