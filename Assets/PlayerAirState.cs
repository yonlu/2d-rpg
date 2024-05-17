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
        if (context.player.IsWallDetected())
            context.stateMachine.ChangeState(context.player.wallSlideState);

        if (context.player.IsGroundDetected())
            context.stateMachine.ChangeState(context.player.idleState);

        if (context.HorizontalInput != 0)
            context.player.SetVelocity(context.player.moveSpeed * .8f * context.HorizontalInput, context.player.rb.velocity.y);
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
