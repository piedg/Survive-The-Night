using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.enabled = false;
        stateMachine.Controller.enabled = false;
        ZombieSpawner.ZombiesInScene.Remove(stateMachine.gameObject);
        GameObject.Destroy(stateMachine.gameObject, 3f);
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
   
}
