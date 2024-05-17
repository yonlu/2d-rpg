using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : IPlayerState
{
    private PlayerContext context;

    private string animBoolName = "Attack";

    public PlayerPrimaryAttackState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        context.SetAnimation(animBoolName, true);
    }

    public void Update()
    {
        if (context.triggerCalled)
            context.stateMachine.ChangeState(context.player.idleState);
    }

    public void Exit()
    {
        context.SetAnimation(animBoolName, false);
    }
}
