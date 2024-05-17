using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IPlayerState
{
    private PlayerContext context;
    private string animBoolName = "WallSlide";

    public PlayerWallSlideState(PlayerContext context) {
        this.context = context;
    }

    public void Enter()
    {
        context.SetAnimation(animBoolName, true);
    }

    public void Update()
    {
        if (context.JumpInput && context.player.facingDir != context.HorizontalInput)
        {
            context.stateMachine.ChangeState(context.player.wallJumpState);
            return;
        }

        if (context.HorizontalInput != 0 && context.player.facingDir != context.HorizontalInput) {
            context.stateMachine.ChangeState(context.player.wallJumpState);
            return;
        }

        if (context.VerticalInput < 0)
            context.player.rb.velocity = new Vector2(0, context.player.rb.velocity.y);

        if (context.VerticalInput >= 0)
            context.player.rb.velocity = new Vector2(0, context.player.rb.velocity.y * .7f);

        if (context.player.IsGroundDetected())
            context.stateMachine.ChangeState(context.player.idleState);
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
