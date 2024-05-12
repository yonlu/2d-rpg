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
    }

    public void Update()
    {
        if (context.HorizontalInput != 0) {
            context.stateMachine.ChangeState(context.player.moveState);
        }

        if (context.JumpInput && context.player.IsGroundDetected()) {
            context.stateMachine.ChangeState(context.player.jumpState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
