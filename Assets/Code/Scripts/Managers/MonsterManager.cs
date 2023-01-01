using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    readonly List<GameObject> totalSpawnedEnemies = new List<GameObject>();
    [SerializeField]
    private GameObject portalPrefab;
    private bool isAppRunning = true;
    public ArtifactInventory monsterInventory;
    public List<Rarity> rarities;

    void OnApplicationQuit()
    {
        isAppRunning = false;
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        totalSpawnedEnemies.Remove(enemy);

        if (totalSpawnedEnemies.Count == 0 && isAppRunning)
        {
            portalPrefab.transform.SetPositionAndRotation(enemy.transform.position + new Vector3(0, 5f), Quaternion.identity);
            portalPrefab.SetActive(true);
        }
    }

    public void FindAndSetEnemies()
    {
        FindAllEnemies()
            .AddListeners()
            .StartInventory()
            .AddArtifactItems();
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

    public MonsterManager StartInventory()
    {
        totalSpawnedEnemies.ForEach(monster =>
        {
            monster.GetComponent<InventoryManager>().StartInventory();
        });
        return this;
    }

    private MonsterManager AddArtifactItems()
    {
        for (int i = 0; i < monsterInventory.Items.Count; i++)
        {
            ArtifactInventoryItem randomArtifact = PickRandomArtifact(monsterInventory.Items);
            for (int j = 0; j < totalSpawnedEnemies.Count; j++)
            {
                totalSpawnedEnemies[j].GetComponent<InventoryManager>().inventory.Add(randomArtifact);
            }
        }

        return this;
    }

    private ArtifactInventoryItem PickRandomArtifact(List<ArtifactInventoryItem> inventory)
    {
        ArtifactInventoryItem item = inventory[Random.Range(0, inventory.Count)];
        return item;
    }
}
