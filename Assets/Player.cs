using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;

    [Header("Dash Info")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir = 1f;
    [SerializeField] public float dashCooldown;
    public float dashCooldownTimer;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

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
    public PlayerDashState dashState  { get; private set; }
    public PlayerWallSlideState wallSlideState  { get; private set; }
    public PlayerWallJumpState wallJumpState  { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
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
        dashState = new PlayerDashState(context);
        wallSlideState = new PlayerWallSlideState(context);
        wallJumpState = new PlayerWallJumpState(context);
        primaryAttackState = new PlayerPrimaryAttackState(context);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        context.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    public void AnimationTrigger() => context.triggerCalled = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckDistance);
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
            return;
        }

        if (x < 0 && facingRight)
        {
            Flip();
            return;
        }
    }
}
