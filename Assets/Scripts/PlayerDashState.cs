using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashDir;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {}

    public override void Enter() {
        base.Enter();

        dashDir = player.facingDir;

        if (previousState is PlayerWallSlideState) {
            dashDir = player.facingDir * -1;
        }
        stateTimer = player.dashDuration;
    }

    public override void Exit() {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update() {
        base.Update();

        player.SetVelocity(player.dashSpeed * dashDir, 0);

        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
    }
}
