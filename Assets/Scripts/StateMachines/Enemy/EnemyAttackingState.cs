using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        //stateMachine.AttackPoint.SetAttack(stateMachine.AttackDamage);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }

        RotateToPlayer(deltaTime);

        if (IsPlayingAnimation(stateMachine.Animator)) { return; }

            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

    public override void Exit() { }

   
}
