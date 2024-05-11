using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private Player player;
    private PlayerStateMachine stateMachine;
    private string animBoolName = "Idle";

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Update()
    {
        Debug.Log("Update Idle State");

        if (Input.GetKeyDown(KeyCode.N)) {
            stateMachine.ChangeState(player.moveState);
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
