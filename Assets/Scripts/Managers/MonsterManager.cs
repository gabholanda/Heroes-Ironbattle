using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    readonly List<GameObject> totalSpawnedEnemies = new List<GameObject>();
    public GameObject towerPrefab;
    private bool isAppRunning = true;

    void OnApplicationQuit()
    {
        isAppRunning = false;
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        totalSpawnedEnemies.Remove(enemy);

        if (totalSpawnedEnemies.Count == 0 && isAppRunning)
        {
            Instantiate(towerPrefab, enemy.transform.position + new Vector3(0, 5f), Quaternion.identity);
        }
    }

    public MonsterManager FindAllEnemies()
    {
        totalSpawnedEnemies.AddRange(GameObject.FindGameObjectsWithTag("Monster"));
        return this;
    }

    public MonsterManager AddListeners()
    {
        totalSpawnedEnemies.ForEach(enemy => enemy.GetComponent<MonsterEvents>().MonsterDeathEvent += OnEnemyDeath);
        return this;
    }
}
