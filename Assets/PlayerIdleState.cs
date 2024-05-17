using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
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
        context.player.rb.velocity = new Vector2(0, 0);
        context.triggerCalled = false;
    }

    public void Update()
    {
        if (context.HorizontalInput == context.player.facingDir && context.player.IsWallDetected())
            return;

        if (context.PrimaryAttackInput)
            context.stateMachine.ChangeState(context.player.primaryAttackState);

        if (context.JumpInput && context.player.IsGroundDetected())
            context.stateMachine.ChangeState(context.player.jumpState);

        if (context.HorizontalInput != 0)
            context.stateMachine.ChangeState(context.player.moveState);
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
