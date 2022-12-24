using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")] public CharacterController Controller { get; private set; }
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField, Header("Movement Settings")] public float DefaultMovementSpeed { get; private set; }
    [field: SerializeField] public float DefaultRotationSpeed { get; private set; }
    [field: SerializeField, Header("Shooting Settings")] public Transform FirePoint { get; private set; }
    [field: SerializeField] public Transform WeaponHolder { get; private set; }
    [field: SerializeField] public GameObject WeaponPrefab { get; set; }
    [field: SerializeField] public Weapon CurrentWeapon { get; private set; }
    [field: SerializeField] public ObjectPool ProjectilePool { get; private set; }
    [field: SerializeField] public GameObject Flashlight { get; private set; }
    [field: SerializeField] public ParticleSystem MuzzleFX { get; private set; }

    public Action OnPickWeapon;

    private void Start()
    {
        MuzzleFX.transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);

        SetDefaultWeapon();

        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetDefaultWeapon()
    {
        Instantiate(WeaponPrefab, WeaponHolder);
        CurrentWeapon = WeaponHolder.GetComponentInChildren<Weapon>();
    }

    public void SetCurrentWeapon()
    {

        Destroy(CurrentWeapon.gameObject);
        GameObject weapon = Instantiate(WeaponPrefab, WeaponHolder);
        WeaponPrefab = weapon;
        CurrentWeapon = weapon.GetComponent<Weapon>();
    }

    private void OnEnable()
    {
       Health.OnTakeDamage += HandleTakeDamage;
       Health.OnDie += HandleDie;

       OnPickWeapon += SetCurrentWeapon;

       InputManager.PauseEvent += HandlePause;
    }

    private void OnDisable()
    {
       Health.OnTakeDamage -= HandleTakeDamage;
       Health.OnDie -= HandleDie;

       OnPickWeapon -= SetCurrentWeapon;

       InputManager.PauseEvent -= HandlePause;
    }

    void HandleTakeDamage()
    {
        UIManager.Instance.SetDamageBgColor();
    }

    void HandleDie()
    {
        if(Health.IsDead)
        {
            UIManager.Instance.EnableDeathPanel(true);
            SwitchState(new PlayerDeadState(this));
        }
    }

    void HandlePause()
    {
        GameManager.Instance.IsPause = true;
    }
}
