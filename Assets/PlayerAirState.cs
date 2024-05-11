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
        Debug.Log("Enter Air State");
    }

    public void Update()
    {
        if (context.player.rb.velocity.y == 0)
        {
            context.stateMachine.ChangeState(context.player.idleState);
        }
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
        Debug.Log("Leave Air State");
    }

}
