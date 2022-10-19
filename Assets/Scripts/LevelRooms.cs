using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRooms : MonoBehaviour
{
    public GameObject player;
    public RoomGenerator generator;
    public TileMapVisualizer tileMap;
    public GameObject trigger;
    public GameObject chest;
    public GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
        generator.GenerateDungeon();
        StartRoom();
        createEnemySpawner(1, 1);
        createEnemySpawner(3, 5);
        createChestRoom(2);
        createExit(4);
    }

    private void createExit(int roomPosition)
    {
        BoundsInt currentRoom = generator.roomsList[roomPosition];
        exit = Instantiate(Resources.Load("Prefabs/Teleport") as GameObject, currentRoom.center, Quaternion.Euler(0, 0, 0));
    }

    private void createChestRoom(int roomPosition)
    {
        BoundsInt currentRoom = generator.roomsList[roomPosition];
        chest = Instantiate(Resources.Load("Prefabs/Chest") as GameObject, currentRoom.center, Quaternion.Euler(0, 0, 0));
    }

    private void StartRoom()
    {
        player.transform.position = generator.roomsList[0].center;
        
    }
    private void createEnemySpawner(int roomPosition, int count)
    {
        BoundsInt currentRoom = generator.roomsList[roomPosition];
        trigger = Instantiate(Resources.Load("Prefabs/Trigger") as GameObject, currentRoom.center, Quaternion.Euler(0, 0, 0));
        trigger.GetComponent<EnemyAndCloseArea>().generator = generator;
        trigger.GetComponent<EnemyAndCloseArea>().walls = tileMap.wallTileMap;
        trigger.GetComponent<BoxCollider2D>().size = (Vector2Int)currentRoom.size;
        trigger.GetComponent<EnemyAndCloseArea>().countOfEnemies = count;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
