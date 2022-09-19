using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    Transform[] spawnPoints;
    public GameObject Zombie;
    public static List<GameObject> ZombiesInScene = new List<GameObject>();
    public float time;
    public int maxZombiesSpawn = 25;
    int currentZombieToSpawn = 1;
    
    void Start()
    {
        ZombiesInScene.Clear();
        spawnPoints = GetComponentsInChildren<Transform>();

        StartCoroutine(Spawn(time));
    }

    IEnumerator Spawn(float time)
    {
        while(true)
        {
            if (ZombiesInScene.Count < 20)
            {
                float randomNumber = Random.Range(1, currentZombieToSpawn);
                for (int i = 1; i <= randomNumber; i++)
                {
                    GameObject zombie = Instantiate(Zombie, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                    ZombiesInScene.Add(zombie);
                }
                currentZombieToSpawn++;
                yield return new WaitForSeconds(time);
            }
            else
            {
                yield return null;
            }
        }
    }
}
