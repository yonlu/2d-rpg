using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public Player player;
    public PlayerStateMachine stateMachine;
    public Animator animator;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool PrimaryAttackInput { get; private set; }
    public float stateTimer;
    public bool triggerCalled;

    public PlayerContext(Player player, PlayerStateMachine stateMachine, Animator animator)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    public void Update()
    {
        stateTimer -= Time.deltaTime;
        player.dashCooldownTimer -= Time.deltaTime;

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        DashInput = Input.GetKeyDown(KeyCode.LeftShift);
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        PrimaryAttackInput = Input.GetKeyDown(KeyCode.Mouse0);

        if (DashInput && player.IsWallDetected())
            return;

        if (DashInput && player.dashCooldownTimer < 0) {
            player.dashCooldownTimer = player.dashCooldown;
            player.dashDir = Input.GetAxisRaw("Horizontal");
            player.stateMachine.ChangeState(player.dashState);
        }

        player.anim.SetFloat("yVelocity", player.rb.velocity.y);
    }

    public void SetAnimation(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }

    public void SetStateTimer(float time)
    {
        stateTimer = time;
    }

    public void AnimationFinishTrigger()
    {
        Debug.Log("[PLAYER_CONTEXT] Animation Finish Trigger Called! triggerCalled: " + triggerCalled);
        triggerCalled = true;
    }
}
