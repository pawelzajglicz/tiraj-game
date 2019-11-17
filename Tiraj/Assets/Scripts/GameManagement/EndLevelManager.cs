using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelManager : GameManagerBase
{

    public int timeForQuit = 3;
    public LevelLoader levelLoader;

    public override void ManagePlayerDeath(int deathPoints)
    {
        DecreaseScoreAmount(deathPoints);
        StartCoroutine(ProcessEnding());
    }

    internal void DecreaseScoreAmount(int v)
    {
        GameSessionInfo.GetInstance().RemovePoints(v);
    }


    IEnumerator ProcessEnding()
    {
        yield return new WaitForSeconds(timeForQuit);
        levelLoader.QuitGame();
    }

    public override void PlayerEnteredPortal()
    {
        levelLoader.LoadMainMenu();
    }
}
