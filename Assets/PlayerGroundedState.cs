using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : IPlayerState
{

    private PlayerContext context;

    public PlayerGroundedState(PlayerContext context)
    {
        this.context = context;
    }

    public void Enter()
    {
        Debug.Log("Enter Grounded State");
    }

    public void Update()
    {
        Debug.Log("Update Grounded State");
    }

    public void Exit()
    {
        Debug.Log("Leave Grounded State");
    }
}
