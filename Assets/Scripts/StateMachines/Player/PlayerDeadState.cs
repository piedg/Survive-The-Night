using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(stateMachine);
        stateMachine.enabled = false;
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }

  
}
