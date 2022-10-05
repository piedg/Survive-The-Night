using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int RifleLocomotionHash = Animator.StringToHash("RifleLocomotion");
    private readonly int PistolLocomotionHash = Animator.StringToHash("PistolLocomotion");
    private readonly int FreeLookForwardHash = Animator.StringToHash("Forward");
    private readonly int FreeLookRightHash = Animator.StringToHash("Right");

    private const float CrossFadeDuration = 0.1f;
    private float nextFire;

    Vector3 direction;

    float forwardAmount;
    float rightAmount;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputManager.ActionEvent_1 += OnAction_1;
    }

    public override void Tick(float deltaTime)
    {
        if(GameManager.Instance.IsPause) { return; }

        SetLocomotion();

        direction = (stateMachine.InputManager.MovementValue.y * Vector3.forward) + (stateMachine.InputManager.MovementValue.x * Vector3.right);

        direction.Normalize();

        ConvertDirection(direction);

        Move(direction * stateMachine.DefaultMovementSpeed, deltaTime);

        FaceToMouse(deltaTime);

        OnShoot();

        stateMachine.Animator.SetFloat(FreeLookForwardHash, forwardAmount, CrossFadeDuration, deltaTime);
        stateMachine.Animator.SetFloat(FreeLookRightHash, rightAmount, CrossFadeDuration, deltaTime);
    }

    public override void Exit() {
        stateMachine.InputManager.ActionEvent_1 -= OnAction_1;
    }

    void ConvertDirection(Vector3 direction)
    {
        if (direction.magnitude > 1)
        { 
            direction.Normalize();
        }

        Vector3 localMove = stateMachine.transform.InverseTransformDirection(direction);

        rightAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    private void FaceToMouse(float deltaTime)
    {
        // Handle player rotation to mouse position
        if (Keyboard.current != null && Mouse.current != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(stateMachine.InputManager.MouseValue);
            Plane virtualPlane = new Plane(Vector3.up, stateMachine.transform.position);

            if (virtualPlane.Raycast(ray, out float hitDist))
            {
                Vector3 hitPoint = ray.GetPoint(hitDist);

                var targetRotation = Quaternion.LookRotation(hitPoint - stateMachine.transform.position);

                // Smoothly rotate towards the target point.
                stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.DefaultRotationSpeed * deltaTime);
            }
        }
        else if (Gamepad.current.IsPressed() && !Mouse.current.IsPressed()) 
        {
            // Gamepad
            if(stateMachine.InputManager.MouseValue == Vector2.zero) { return; }

             Vector3 direction = new Vector3(stateMachine.InputManager.MouseValue.x, 0, stateMachine.InputManager.MouseValue.y);
             stateMachine.transform.rotation = Quaternion.LookRotation(direction);

            var targetRotation = Quaternion.LookRotation(direction - stateMachine.transform.position);

            // Smoothly rotate towards the target point.
            stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.DefaultRotationSpeed * deltaTime);
        }
    }

    private void OnShoot()
    {
        if (stateMachine.InputManager.IsShooting)
        {
            if (Time.fixedTime > nextFire)
            {
                nextFire = Time.fixedTime + stateMachine.CurrentWeapon.FiringRate;

                GameObject projectile = stateMachine.ProjectilePool.GetObjectFromPool();

                projectile.transform.SetPositionAndRotation(stateMachine.FirePoint.transform.position, stateMachine.FirePoint.transform.rotation);

                projectile.GetComponent<Damage>().SetAttack(stateMachine.CurrentWeapon.Damage);
                projectile.SetActive(true);

                //stateMachine.FireFX.gameObject.SetActive(true);
                AudioController.Instance.PlayClip(stateMachine.CurrentWeapon.FireSFX);
            }
        }
        else
        {
            //stateMachine.FireFX.gameObject.SetActive(false);
        }
    }

    private void OnAction_1()
    {
        stateMachine.Flashlight.SetActive(!stateMachine.Flashlight.activeSelf);
    }

    private void SetLocomotion()
    {
        switch (stateMachine.CurrentWeapon.Type)
        {
            case eWeaponType.Rifle:
                stateMachine.Animator.Play(RifleLocomotionHash);
                break;
            case eWeaponType.Pistol:
                stateMachine.Animator.Play(PistolLocomotionHash);
                break;
            default:
                break;
        }
    }
}
