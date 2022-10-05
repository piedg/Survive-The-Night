using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject ObjectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateMachine>().WeaponPrefab = ObjectPrefab;
            other.GetComponent<PlayerStateMachine>().OnPickWeapon?.Invoke();
            Destroy(gameObject);
        }
    }
}
