using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerContext context;
    private string animBoolName = "Move";

    public PlayerMoveState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.SetAnimation(animBoolName, true);
        Debug.Log("Enter Move State");
    }

    public void Update()
    {
        Debug.Log("Update Move State");
        context.player.SetVelocity(context.HorizontalInput * context.player.moveSpeed, context.player.rb.velocity.y);

        if (context.HorizontalInput == 0) {
            context.stateMachine.ChangeState(context.player.idleState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
        Debug.Log("Exit Move State");
    }
}
