using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapItems", menuName = "ScriptableObjects/Map/Map Items")]
public class MapItems : ScriptableObject
{
    public Tile[] outerTiles;
    public Tile[] bottomTiles;
    public Tile[] middleTiles;
    public Tile[] topTiles;
    public MapType type;
}
