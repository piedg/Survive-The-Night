using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    Transform[] spawnPoints;
    Dictionary<string, Transform> spawnMap = new Dictionary<string, Transform>();
    public GameObject Zombie;
    public ObjectPool ZombiePool;
    public static List<GameObject> ZombiesInScene = new List<GameObject>();
    public float time;
    public int maxZombiesSpawn = 25;
    public int currentZombieToSpawn = 1;
    
    void Start()
    {
        ZombiesInScene.Clear();

        spawnPoints = GetComponentsInChildren<Transform>();

        foreach (var spawnPoint in spawnPoints)
        {
            spawnMap.Add(spawnPoint.name, spawnPoint);
        }

        StartCoroutine(Spawn(time));
    }

    IEnumerator Spawn(float time)
    {
        while(!GameManager.Instance.IsEndGame)
        {
            if (ZombiesInScene.Count < maxZombiesSpawn)
            {
                float randomNumber = Random.Range(1, currentZombieToSpawn);
                for (int i = 1; i <= randomNumber; i++)
                {
                    GameObject zombie = ZombiePool.GetObjectFromPool();
                    zombie.transform.SetPositionAndRotation(GetRandomSpawnPoint(spawnPoints), Quaternion.identity);

                    zombie.SetActive(true);
                    ZombiesInScene.Add(zombie);
                }
                currentZombieToSpawn++;
                Debug.Log("Zombie in scene " + ZombiesInScene.Count + " CurrentZombieToSpawn " + currentZombieToSpawn);
                yield return new WaitForSeconds(time);
            }
            else
            {
                currentZombieToSpawn = maxZombiesSpawn;
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
    public void ActiveSpawnPoint(string name)
    {
        spawnMap[name].gameObject.SetActive(true);
    }

}
