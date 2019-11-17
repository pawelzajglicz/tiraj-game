using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    private static GameManagerBase instance;

 

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<GameManagerBase>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManagerBase GetInstance()
    {
        if (instance == null)
        {
            WalkingLevelManager findedInstance = FindObjectOfType<WalkingLevelManager>();
            instance = findedInstance;
        }

        if (instance == null)
        {
            InvasionLevelManager findedInstance = FindObjectOfType<InvasionLevelManager>();
            instance = findedInstance;
        }

        if (instance == null)
        {
            EndLevelManager findedInstance = FindObjectOfType<EndLevelManager>();
            instance = findedInstance;
        }

        return instance;
    }

    virtual public void ManagePlayerDeath(int pointsForDeath)
    {
        
    }

    virtual public void RemoveGravitySwitch()
    {

    }

    virtual public void ManagePlayerSuccess(int pointsForSuccess)
    {

    }

    virtual public void PlayerEnteredPortal()
    {

    }

    virtual public Player BringNewAlien()
    {
        return null;
    }

    virtual public void ManageTimeEnd()
    {

    }

    virtual public bool IsGravitySwitchAllowed()
    {
        return true;
    }

}
