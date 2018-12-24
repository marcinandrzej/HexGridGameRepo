using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStates
{ 
    void OnEnter(GameManagerScript gameMan);

    void Execute(GameObject hex, int players);

    void OnExit();
}
