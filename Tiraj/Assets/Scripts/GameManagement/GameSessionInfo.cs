using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionInfo : MonoBehaviour
{
    private static GameSessionInfo instance;

    public int startLifes = 3;
    public int leftLifes;
    public int startGravitySwitches = 2;
    public int gravitySwitchesLeft;
    public int startPoints = 0;
    public int currentPoints = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<GameSessionInfo>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameSessionInfo GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftLifes = startLifes;
        gravitySwitchesLeft = startGravitySwitches;

        DontDestroyOnLoad(this);
    }
    

    internal void RemoveLife()
    {
        leftLifes--;
    }

    internal int GetLifesLeft()
    {
        return leftLifes;
    }

    internal void RemoveGravitySwitch()
    {
        gravitySwitchesLeft--;
    }

    internal int GetGravitySwitchesLeft()
    {
        return gravitySwitchesLeft;
    }

    internal void RemovePoints(int v)
    {
        currentPoints -= v;
    }

    internal int GetCurrentPoints()
    {
        return currentPoints;
    }

    internal void AddPoints(int v)
    {
        currentPoints += v;
    }

    internal void AddGravitySwitch()
    {
        gravitySwitchesLeft++;
    }

    internal void AddLife()
    {
        leftLifes++;
    }

    public void Reset()
    {
        leftLifes = startLifes;
        gravitySwitchesLeft = startGravitySwitches;
    }
}
