using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    private PlayerContext context;
    private string animBoolName = "Jump";

    public PlayerJumpState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.player.rb.velocity = new Vector2(context.player.rb.velocity.x, context.player.jumpForce); 
        context.SetAnimation(animBoolName, true);
        Debug.Log("Enter Jump State");
    }

    public void Update()
    {
        if (context.player.rb.velocity.y < 0)
        {
            context.stateMachine.ChangeState(context.player.airState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
        Debug.Log("Exit Jump State");
    }

}
