using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public enum MapType
{
    Florest,
    Dungeon
}
public class MapManager : MonoBehaviour
{
    [SerializeField]
    MapGenerator mapGenerator;
    public GameObject player;
    public List<GameObject> enemies;
    public List<GameObject> enemiesToSpawn;
    public List<MapItems> availableMapItems;
    public Tilemap floorTilemap;
    public Tilemap middleTilemap;
    public Tilemap topTilemap;
    public int minSize, maxSize;
    public AstarPath astarPath;

    [SerializeField]
    private MonsterManager enemyManager;

    private void Awake()
    {
        //StartCoroutine(Automap());
        StartGeneratingMap();
    }
    public void StartGeneratingMap()
    {
        SetTilemaps()
        .PickGenerator()
        .GenerateMap()
        .SpawnPlayer()
        .SetEnemiesToSpawn()
        .SpawnEnemies()
        .Pathfind();

        enemyManager
            .FindAllEnemies()
            .AddListeners();
    }

    IEnumerator Automap()
    {
        while (true)
        {
            StartGeneratingMap();
            yield return new WaitForSeconds(10f);
        }
    }

    private MapManager SetTilemaps()
    {
        mapGenerator.floorTilemap = floorTilemap;
        mapGenerator.middleTilemap = middleTilemap;
        mapGenerator.topTilemap = topTilemap;
        return this;
    }

    private MapManager PickGenerator()
    {
        switch (mapGenerator.mapItems.type)
        {
            case MapType.Florest:
                mapGenerator = (FlorestGenerator)mapGenerator;
                mapGenerator.mapItems = availableMapItems.Find((mapItems) => mapItems.type == MapType.Florest);
                break;
            case MapType.Dungeon:
                break;
        }
        return this;
    }

    private MapManager GenerateMap()
    {
        mapGenerator
          .ClearMap()
          .SetMaxCoords(minSize, maxSize)
          .SetCoords()
          .SetPlayerSpawnPoint()
          .SetEnemiesSpawnPoints()
          .FillMap()
          .ClearCoords();
        return this;
    }

    private MapManager SpawnPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = mapGenerator.playerSpawnPoint;
            }
        }
        else
        {
            Instantiate(player, new Vector3(mapGenerator.playerSpawnPoint.x, mapGenerator.playerSpawnPoint.y), Quaternion.identity);
        }
        return this;
    }
    private MapManager SetEnemiesToSpawn()
    {
        //enemiesToSpawn = enemies.FindAll(enemy => enemy.GetComponent<EnemyController>().type == mapGenerator.mapItems.type);
        enemiesToSpawn = enemies.FindAll(enemy => enemy);
        return this;
    }

    public MapManager SpawnEnemies()
    {
        int enemiesQty = Random.Range(5, 15);
        for (int i = 0; i < enemiesQty; i++)
        {
            Instantiate(GetRandomEnemy(), GetRandomEnemySpawnPoint(), Quaternion.identity);
        }
        return this;
    }
    private GameObject GetRandomEnemy()
    {
        return enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
    }
    private Vector3 GetRandomEnemySpawnPoint()
    {
        return mapGenerator.enemiesSpawnPoints[Random.Range(0, mapGenerator.enemiesSpawnPoints.Count - 1)];
    }

    private void Pathfind()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        yield return new WaitForSeconds(0.5f);
        astarPath.Scan();
    }
}
