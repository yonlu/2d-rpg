using System.Collections;
using System.Collections.Generic;
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
        context.player.SetVelocity(context.player.dashSpeed * context.player.facingDir, context.player.rb.velocity.y);

        if (context.stateTimer < 0) {
            context.stateMachine.ChangeState(context.player.idleState);
        }
    }

    public void Exit()
    {
        context.player.SetVelocity(0, context.player.rb.velocity.y);
        context.SetAnimation(animBoolName, false);
    }

}
