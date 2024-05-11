using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public Player player;
    public PlayerStateMachine stateMachine;
    public Animator animator;
    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }

    public PlayerContext(Player player, PlayerStateMachine stateMachine, Animator animator)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    public void UpdateInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (player.IsGroundDetected()) {
            JumpInput = Input.GetKeyDown(KeyCode.Space);
        }
        player.anim.SetFloat("yVelocity", player.rb.velocity.y);
    }

    public void SetAnimation(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }
}
