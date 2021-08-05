using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class MapGenerator : ScriptableObject
{
    // TODO: Refactor to use x,y list/array
    public List<Coordinates> outerCoords = new List<Coordinates>();
    public List<Coordinates> coords = new List<Coordinates>();
    public List<Coordinates> innerCoords = new List<Coordinates>();
    public List<Coordinates> obstaclesCoords = new List<Coordinates>();
    public List<Vector3> enemiesSpawnPoints = new List<Vector3>();
    public Vector3 playerSpawnPoint { get; set; }
    public MapItems mapItems;
    public Tilemap floorTilemap { get; set; }
    public Tilemap middleTilemap { get; set; }
    public Tilemap topTilemap { get; set; }
    protected int maxCoords;

    public abstract MapGenerator SetMaxCoords(int minSize, int maxSize);
    public abstract MapGenerator SetCoords();
    public abstract MapGenerator FillMap();
    public abstract MapGenerator SetPlayerSpawnPoint();
    public abstract MapGenerator SetEnemiesSpawnPoints();

    public MapGenerator ClearMap()
    {
        floorTilemap.ClearAllTiles();
        middleTilemap.ClearAllTiles();
        topTilemap.ClearAllTiles();
        return this;
    }

    public MapGenerator ClearCoords()
    {
        outerCoords = new List<Coordinates>();
        coords = new List<Coordinates>();
        innerCoords = new List<Coordinates>();
        obstaclesCoords = new List<Coordinates>();
        return this;
    }

    public bool NotObstacleCoordinate(Coordinates someCoord, Coordinates obstacleCoord)
    {
        return someCoord.x != obstacleCoord.x || someCoord.y != obstacleCoord.y;
    }

}
