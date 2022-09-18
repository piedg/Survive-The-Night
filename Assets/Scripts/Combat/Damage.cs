using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Collider CharacterCollider;
    private int damage;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == CharacterCollider && CharacterCollider != null) { return; }

        if(!gameObject.CompareTag("Bullet"))
        {
            if (alreadyCollidedWith.Contains(other)) { return; }
            alreadyCollidedWith.Add(other);
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
