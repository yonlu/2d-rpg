using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;

#region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
#endregion

#region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerGroundedState groundedState { get; private set; }
#endregion

    public PlayerContext context { get; private set; }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new PlayerStateMachine();
        context = new PlayerContext(this, stateMachine, anim);

        idleState = new PlayerIdleState(context);
        moveState = new PlayerMoveState(context);
        jumpState = new PlayerJumpState(context);
        airState = new PlayerAirState(context);
        groundedState = new PlayerGroundedState(context);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        context.UpdateInput();
        stateMachine.currentState.Update();

        if (context.JumpInput)
        {
            stateMachine.ChangeState(jumpState);
        }
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
