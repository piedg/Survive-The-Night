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
    public int currentZombieToSpawn = 1;
    
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
            if (ZombiesInScene.Count < maxZombiesSpawn)
            {
                float randomNumber = Random.Range(Mathf.Min(1, currentZombieToSpawn), currentZombieToSpawn);
                for (int i = 1; i <= randomNumber; i++)
                {
                    GameObject zombie = Instantiate(
                        Zombie,
                        GetRandomSpawnPoint(spawnPoints), 
                        Quaternion.identity);

                    ZombiesInScene.Add(zombie);
                }
                currentZombieToSpawn++;
                yield return new WaitForSeconds(time);
            }
            else
            {
                Debug.Log("Stop spawn, Zombie in Scene: " + ZombiesInScene.Count);
                yield return null;
            }
        }
    }

    Vector3 GetRandomSpawnPoint(Transform[] spawnPoints)
    {
        Vector3 randomSpawnPointPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        float randomX = Random.Range(randomSpawnPointPos.x - 2f, randomSpawnPointPos.x + 2f);
        float randomZ = Random.Range(randomSpawnPointPos.z - 2f, randomSpawnPointPos.z + 2f);

        return new Vector3(randomX, randomSpawnPointPos.y, randomZ);
    }

}
