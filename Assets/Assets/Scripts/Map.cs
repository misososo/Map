using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] Tilemap wallTileMap;
    [SerializeField] Tile wall;

    [SerializeField] Tilemap roomTileMap;
    [SerializeField] Tile room;
    //[SerializeField] Tile startRoom;
    //[SerializeField] Tile goalRoom;

    [SerializeField] Tilemap miniMapTileMap;
    [SerializeField] Tile miniMapRoom;

    [SerializeField] int roomNum;
    [SerializeField] int mapHoriLength;
    [SerializeField] int mapVerLength;
    [SerializeField] int enemyRoomNum;

    [SerializeField] Room startRoom;
    [SerializeField] Room goalRoom;
    [SerializeField] Room enemyRoom;

    [SerializeField] GameObject player;
    Vector3 playerStartPos;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < mapVerLength; ++i)
        {
            for(int j = 0; j < mapHoriLength; ++j)
            {
                wallTileMap.SetTile(new Vector3Int(j, i, 0), wall);
            }          
        }

        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap()
    {
        int randomX = Random.Range(0, mapHoriLength);
        int randomY = Random.Range(0, mapVerLength);

        CreateRoom(randomX, randomY);            
        CreateWall();
        SettingRoomType();
        player.transform.position = playerStartPos;
    }

    void CreateRoom(int posX, int posY, int dirX = 0, int dirY = 0, int moveDis = 1)
    {
        if(roomNum <= 0)return;

        if (wallTileMap.HasTile(new Vector3Int(posX, posY, 0)))
        {
            roomNum--;
            moveDis--;

            wallTileMap.SetTile(new Vector3Int(posX, posY, 0), null);
            roomTileMap.SetTile(new Vector3Int(posX, posY, 0), room);
            miniMapTileMap.SetTile(new Vector3Int(posX, posY, 0), miniMapRoom);

            //Instantiate(cameraPoint, roomTileMap.GetCellCenterWorld(new Vector3Int(posX, posY, 0)), Quaternion.identity);
        }
        else if(roomTileMap.HasTile(new Vector3Int(posX, posY, 0)))
        {
            moveDis--;            
        }
        else
        {
            moveDis = 0;
        }

        if(moveDis <= 0 ||
            !roomTileMap.HasTile(new Vector3Int(posX + dirX, posY + dirY, 0)) &&
            !wallTileMap.HasTile(new Vector3Int(posX + dirX, posY + dirY, 0)))
        {
            int[] createDirX = { 1, -1, 0, 0 };
            int[] createDirY = { 0, 0, 1, -1 };
            List<Vector2Int> createDir = new List<Vector2Int>();

            moveDis = Random.Range(1, 5);

            for(int i = 0; i < createDirX.Length; ++i)
            {
                if (!roomTileMap.HasTile(new Vector3Int(posX + createDirX[i], posY + createDirY[i], 0)) &&
                    !wallTileMap.HasTile(new Vector3Int(posX + createDirX[i], posY + createDirY[i], 0)) ||
                    dirX == createDirX[i] && dirY == createDirY[i])
                {
                    continue;
                }
              
                createDir.Add(new Vector2Int(createDirX[i], createDirY[i]));              
            }

            int random = Random.Range(0, createDir.Count);
          
            dirX = createDir[random].x;
            dirY = createDir[random].y;
        }

        CreateRoom(posX + dirX, posY + dirY, dirX, dirY, moveDis);       
    }

    void CreateWall()
    {
        for (int i = -1; i < mapHoriLength + 1; ++i)
        {
            wallTileMap.SetTile(new Vector3Int(i, -1, 0), wall);
            wallTileMap.SetTile(new Vector3Int(i, mapHoriLength, 0), wall);
        }

        for (int i = -1; i < mapVerLength + 1; ++i)
        {
            wallTileMap.SetTile(new Vector3Int(-1, i, 0), wall);
            wallTileMap.SetTile(new Vector3Int(mapVerLength, i, 0), wall);
        }
    }

    void SettingRoomType()
    {
        Room room;
        int roomId = 0;

        List<int> posX = new List<int>();
        List<int> posY = new List<int>();

        for (int i = 0; i < mapVerLength; ++i)
        {
            for (int j = 0; j < mapHoriLength; ++j)
            {
                if (roomTileMap.HasTile(new Vector3Int(j, i, 0)))
                {
                    posX.Add(j);
                    posY.Add(i);
                }
            }
        }
        
        int random = Random.Range(0, posX.Count);

        room = Instantiate(startRoom, roomTileMap.GetCellCenterWorld(new Vector3Int(posX[random], posY[random], 0)), Quaternion.identity);
        room.SetId(roomId);
        roomId++;

        playerStartPos = roomTileMap.GetCellCenterWorld(new Vector3Int(posX[random], posY[random], 0));

        posX.RemoveAt(random);
        posY.RemoveAt(random);

        random = Random.Range(0, posX.Count);

        room = Instantiate(goalRoom, roomTileMap.GetCellCenterWorld(new Vector3Int(posX[random], posY[random], 0)), Quaternion.identity);
        room.SetId(roomId);
        roomId++;

        posX.RemoveAt(random);
        posY.RemoveAt(random);

        for(int i = 0; i < enemyRoomNum; ++i)
        {
            random = Random.Range(0, posX.Count);

            room = Instantiate(enemyRoom, roomTileMap.GetCellCenterWorld(new Vector3Int(posX[random], posY[random], 0)), Quaternion.identity);
            room.SetId(roomId);
            roomId++;
            //Debug.Log(room.GetId());

            posX.RemoveAt(random);
            posY.RemoveAt(random);
        }
    }
}
