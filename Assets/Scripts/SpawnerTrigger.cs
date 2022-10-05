using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    [SerializeField] ZombieSpawner ZombieSpawner;
    [SerializeField] GameObject SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ZombieSpawner.ActiveSpawnPoint(SpawnPoint.transform);
            gameObject.SetActive(false);
        }
    }
}
