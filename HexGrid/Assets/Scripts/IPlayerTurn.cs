using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerTurn : IGameStates
{
    private GameManagerScript gM;

    public void Execute(GameObject hex, int players)
    {
        gM.PlayHex(hex);
        if(players == 1)
            gM.ChangeState(new IComTurn());
    }

    public void OnEnter(GameManagerScript gameMan)
    {
        gM = gameMan;
    }

    public void OnExit()
    {
        
    }
}
