using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerDashState : IPlayerState
{

    private PlayerContext context;
    private string animBoolName = "Dash";

    public PlayerDashState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.SetStateTimer(context.player.dashDuration);
        context.SetAnimation(animBoolName, true);
    }

    public void Update()
    {
        if (context.player.dashDir == 0) {
            context.player.dashDir = context.player.facingDir;
        }

        context.player.SetVelocity(context.player.dashSpeed * context.player.dashDir, 0);

        if (context.stateTimer < 0) {
            context.stateMachine.ChangeState(context.player.airState);
        }
    }

    public void Exit()
    {
        context.player.SetVelocity(context.HorizontalInput * context.player.moveSpeed, context.player.rb.velocity.y);
        context.SetAnimation(animBoolName, false);
    }

}
