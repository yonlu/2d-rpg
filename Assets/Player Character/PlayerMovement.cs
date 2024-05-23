using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum PlayerState {
        Idle,
        Running,
        Airborne
    }

    PlayerState state;
    public bool stateComplete;

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator animator;

    public float acceleration;
    public float maxXSpeed;
    [Range(0f, 1f)]
    public float groundDecay;
    public float jumpAnimationSpeed = 1f;
    public float jumpSpeed;

    public bool grounded;
    public float xInput;
    public float yInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (stateComplete) {
            SelectState();
        }
        UpdateState();
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleXMovement();
        ApplyFriction();
    }   

    void CheckInput() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        if (Input.GetButton("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    void HandleXMovement() {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction() {
        if (grounded && xInput == 0 && body.velocity.y <= 0) {
            body.velocity *= groundDecay;
        }
    }

    void SelectState() {
        stateComplete = false;

        if (grounded) {
            if (xInput == 0) {
                state = PlayerState.Idle;
                StartIdle();
            } else {
                state = PlayerState.Running;
                StartRunning();
            }
        } else {
            state = PlayerState.Airborne;
            StartAirborne();
        }
    }

    void StartIdle() {
        animator.Play("Idle");
    }

    void StartRunning() {
        animator.Play("Run");
    }

    void StartAirborne() {
        animator.Play("Jump");
        animator.speed = jumpAnimationSpeed;
    }

    void UpdateState() {
        switch (state) {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Running:
                UpdateRunning();
                break;
            case PlayerState.Airborne:
                UpdateAirborne();
                break;
        }
    }

    void UpdateIdle() {
        if (!grounded || xInput != 0) {
            stateComplete = true;
        }
    }

    void UpdateRunning() {
        float velX = body.velocity.x;

        animator.speed = Mathf.Abs(velX) / maxXSpeed;

        if (!grounded || Mathf.Abs(velX) < 0.1f) {
            stateComplete = true;
        }
    }

    void UpdateAirborne() {
        float time = Map(body.velocity.y, jumpSpeed, -jumpSpeed, 0, 1, true);
        animator.Play("Jump", 0, time);
        //animator.speed = 0;

        if (grounded) {
            stateComplete = true;
        }
    }

    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false) {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));

        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }

}
