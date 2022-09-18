using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")]
    public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    //[field: SerializeField] public CooldownManager CooldownManager { get; private set; }
    [field: SerializeField, Header("Movement Settings")] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
   // [field: SerializeField, Header("Attack Settings")] public Damage AttackPoint { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    public SkinnedMeshRenderer[] Skins;
    public GameObject Player { get; private set; }

    private void Start()
    {
        Skins = GetComponentsInChildren<SkinnedMeshRenderer>();

        Skins[Random.Range(0, Skins.Length - 1)].enabled = true;

        Player = GameObject.FindGameObjectWithTag("Player");
        SwitchState(new EnemyIdleState(this));
    }

    public void Attack()
    {
        //AttackPoint.gameObject.SetActive(true);
    }

    public void FinishAttack()
    {
    //    AttackPoint.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {

    }

    private void HandleDie()
    {
        // TODO: switch to Die State
        SwitchState(new EnemyDeathState(this));
    }
}
