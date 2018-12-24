using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IComTurn : IGameStates
{
    private GameManagerScript gM;

    public void Execute(GameObject hex, int players)
    {

    }

    public void OnEnter(GameManagerScript gameMan)
    {
        gM = gameMan;
        gM.EnemyMove();
    }

    public void OnExit()
    {

    }
}
