using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private PlayerContext context;
    private string animBoolName = "Idle";

    public PlayerIdleState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.SetAnimation(animBoolName, true);
        Debug.Log("Enter Idle State");
    }

    public void Update()
    {
        Debug.Log("Update Idle State");
        if (context.HorizontalInput != 0) {
            context.stateMachine.ChangeState(context.player.moveState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
        Debug.Log("Exit Idle State");
    }
}
