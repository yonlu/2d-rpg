using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : IPlayerState
{
    private PlayerContext context;
    private string animBoolName = "Jump";

    public PlayerAirState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.SetAnimation(animBoolName, true);
    }

    public void Update()
    {
        if (context.player.IsGroundDetected())
        {
            context.stateMachine.ChangeState(context.player.idleState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
