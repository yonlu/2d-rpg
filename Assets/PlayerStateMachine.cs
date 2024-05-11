using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public IPlayerState currentState;

    public void Initialize(IPlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
