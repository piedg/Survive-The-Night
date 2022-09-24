using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;
    private bool isInvulnerable;

    public event Action OnTakeDamage;
    public event Action OnTakeHeal;
    public event Action OnDie;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void SetFullHealth()
    {
        health = maxHealth;
    }

    public void Heal(int value)
    {
        health = Mathf.Min(health + value, maxHealth);

        OnTakeHeal?.Invoke();

        //Debug.Log("Current health " + health + " heal recevied " + value);
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }
        if (isInvulnerable) { return; }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }

        //Debug.Log("Current health " + health + " damage recevied " + damage);
    }
}
