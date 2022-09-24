using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    float timeToDisable = 5f;
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Controller.enabled = false;
        //GameObject.Destroy(stateMachine.gameObject, 3f);
    }

    public override void Tick(float deltaTime) 
    {
        if(timeToDisable > 0f)
        {
            timeToDisable -= deltaTime;
            Debug.Log(timeToDisable);
        }
        else
        {
            stateMachine.gameObject.SetActive(false);
            ZombieSpawner.ZombiesInScene.Remove(stateMachine.gameObject);
            timeToDisable = 5f;
            stateMachine.Health.SetFullHealth();
            stateMachine.Ragdoll.ToggleRagdoll(false);
            stateMachine.Controller.enabled = true;
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit() { }
   
}
