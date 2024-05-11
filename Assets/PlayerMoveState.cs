using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{

    private Player player;
    private PlayerStateMachine stateMachine;
    private string animBoolName = "Move";

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("Enter Move State");
    }

    public void Update()
    {
        Debug.Log("Update Move State");

        if (Input.GetKeyDown(KeyCode.N)) {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Move State");
    }
}
