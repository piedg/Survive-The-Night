using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
   
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement * stateMachine.MovementSpeed) * deltaTime);
    }

    protected void FaceToPlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void RotateToPlayer(float deltaTime)
    {
        if (stateMachine.Player == null) { return; }

        var targetRotation = Quaternion.LookRotation(stateMachine.Player.transform.position - stateMachine.transform.position);

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.RotationSpeed * deltaTime);
    }

    protected void FaceForward(float deltaTime)
    {
        if (stateMachine.Agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            var targetRotation = Quaternion.LookRotation(stateMachine.Agent.velocity.normalized);

            stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.RotationSpeed * deltaTime);
        }
    }

    protected bool IsInViewRange()
    {
        Vector3 toPlayer = stateMachine.Player.transform.position - stateMachine.transform.position;

        Vector3 localDirection = stateMachine.transform.InverseTransformDirection(toPlayer);

        RaycastHit hit;

        Debug.DrawRay(stateMachine.transform.position + Vector3.up, toPlayer + Vector3.up, Color.red);

        float angle = Mathf.Atan2(localDirection.z, localDirection.x) * Mathf.Rad2Deg - 90;

        if (angle < stateMachine.ViewAngle && angle > -stateMachine.ViewAngle)
        {
            if (Physics.Raycast(stateMachine.transform.position + (Vector3.up / 2), toPlayer + Vector3.up, out hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent(out PlayerStateMachine Player))
                {
                    return Player != null;
                }
            }
        }
        return false;
    }


    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }


}
