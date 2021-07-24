using System;
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

    private void Start()
    {
        StartCoroutine(Automap());
        //StartGeneratingMap();
    }
    public void StartGeneratingMap()
    {
        SetTilemaps()
        .PickGenerator()
        .GenerateMap()
        .SpawnPlayer()
        .SetEnemiesToSpawn()
        .SpawnEnemies();
    }

    IEnumerator Automap()
    {
        while (true)
        {
            StartGeneratingMap();
            yield return new WaitForSeconds(3f);
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
          .ClearCoords()
          .SetMaxCoords(minSize, maxSize)
          .SetCoords()
          .SetPlayerSpawnPoint()
          .SetEnemiesSpawnPoints()
          .FillMap();
        return this;
    }

    private MapManager SpawnPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = new Vector3(mapGenerator.playerSpawnCoordinate.x, mapGenerator.playerSpawnCoordinate.y);
            }
        }
        else
        {
            Instantiate(player, new Vector3(mapGenerator.playerSpawnCoordinate.x, mapGenerator.playerSpawnCoordinate.y), Quaternion.identity);
        }
        return this;
    }
    private MapManager SetEnemiesToSpawn()
    {
        //enemiesToSpawn = enemies.FindAll(enemy => enemy.GetComponent<EnemyController>().type == mapGenerator.mapItems.type);
        return this;
    }

    private MapManager SpawnEnemies()
    {
        int enemiesQty = Random.Range(5, 15);
        for (int i = 0; i < enemiesQty; i++)
        {
            GameObject chosenEnemy = enemies[Random.Range(0, enemies.Count - 1)];
            Coordinates randomSpawn = mapGenerator.enemiesSpawnCoords[Random.Range(0, mapGenerator.enemiesSpawnCoords.Count - 1)];
            Instantiate(chosenEnemy, new Vector3(randomSpawn.x, randomSpawn.y), Quaternion.identity);
        }
        return this;
    }
}
