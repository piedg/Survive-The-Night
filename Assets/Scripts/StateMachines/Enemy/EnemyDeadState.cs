using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    float timeToDisable = 5f;
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Controller.enabled = false;
    }

    public override void Tick(float deltaTime) 
    {
        if (timeToDisable < 0f)
        {
            timeToDisable = 5f;
            stateMachine.Health.SetFullHealth();
            stateMachine.Ragdoll.ToggleRagdoll(false);
            stateMachine.Controller.enabled = true;
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            ZombieSpawner.ZombiesInScene.Remove(stateMachine.gameObject);
            stateMachine.gameObject.SetActive(false);
        }

        timeToDisable -= deltaTime;
    }

    public override void Exit() { }
   
}
