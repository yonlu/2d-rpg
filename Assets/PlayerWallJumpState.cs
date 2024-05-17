using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IPlayerState
{
    private PlayerContext context;

    public PlayerWallJumpState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.player.SetVelocity(5 * -context.player.facingDir, context.player.jumpForce);
    }

    public void Update()
    {
        context.stateMachine.ChangeState(context.player.airState);
    }

    public void Exit()
    {
    }

}
