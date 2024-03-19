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
    [SerializeField] Tile startRoom;
    [SerializeField] Tile goalRoom;

    [SerializeField] Tilemap miniMapTileMap;

    [SerializeField] int roomNum = 5;
    [SerializeField] int mapHoriLength;
    [SerializeField] int mapVerLength;

    [SerializeField] GameObject cameraPoint;

    [SerializeField] GameObject mapEreasParent;
    GameObject[,] mapEreas;

    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject startRoomPrefab;
    [SerializeField] GameObject goalRoomPrefab;
    [SerializeField] GameObject insideWall;
    [SerializeField] GameObject outsideWall;

    

    [SerializeField] GameObject player;
    Vector3 playerStartPos;

    // Start is called before the first frame update
    void Start()
    {
        //mapEreas = new GameObject[mapVerLength, mapHoriLength];

        //int childIndex = 0;

        for(int i = 0; i < mapVerLength; ++i)
        {
            for(int j = 0; j < mapHoriLength; ++j)
            {
                wallTileMap.SetTile(new Vector3Int(j, i, 0), wall);
                //mapEreas[j, i] = mapEreasParent.transform.GetChild(childIndex).gameObject;
                //childIndex++;
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
        SettingStartAndGoal();
        player.transform.position = playerStartPos;
    }

    void CreateRoom(int posX, int posY, int dirX = 0, int dirY = 0, int moveDis = 1)
    {
        if(roomNum <= 0)return;

        //roomNum--;
        if (wallTileMap.HasTile(new Vector3Int(posX, posY, 0)))
        {
            roomNum--;
            moveDis--;
            wallTileMap.SetTile(new Vector3Int(posX, posY, 0), null);
            roomTileMap.SetTile(new Vector3Int(posX, posY, 0), room);
            miniMapTileMap.SetTile(new Vector3Int(posX, posY, 0), room);
            Instantiate(cameraPoint, roomTileMap.GetCellCenterWorld(new Vector3Int(posX, posY, 0)), Quaternion.identity);
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

    bool CheckCreateRoom(int posX, int posY)
    {
        if(posX >= mapHoriLength || posX < 0)return false;
        if(posY >= mapVerLength || posY < 0)return false;        

        return true;
    }

    bool CheckCreateWall(int posX, int posY)
    {
        if (roomTileMap.HasTile(new Vector3Int(posX + 1, posY,     0)) &&
            roomTileMap.HasTile(new Vector3Int(posX - 1, posY,     0)) &&
            roomTileMap.HasTile(new Vector3Int(posX,     posY + 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX,     posY - 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX + 1, posY + 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX + 1, posY - 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX - 1, posY + 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX - 1, posY - 1, 0)) &&
            roomTileMap.HasTile(new Vector3Int(posX,     posY,     0)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateWall()
    {
        //List<int> posX = new List<int>();
        //List<int> posY = new List<int>();

        //GameObject wall;

        /*for (int i = 1; i < mapVerLength - 1; ++i)
        {
            for (int j = 1; j < mapHoriLength - 1; ++j)
            {
                if(CheckCreateWall(j, i))
                {
                    posX.Add(j);
                    posY.Add(i);
                }
            }
        }*/

        /*for(int i = 0; i < posX.Count; ++i)
        {
            //wall = Instantiate(insideWall, mapEreas[posX[i], posY[i]].transform.position, Quaternion.identity);
            //Destroy(mapEreas[posX[i], posY[i]]);
            //mapEreas[posX[i], posY[i]] = wall;
            roomTileMap.SetTile(new Vector3Int(posX[i], posY[i], 0), null);
            wallTileMap.SetTile(new Vector3Int(posX[i], posY[i], 0), wall);
        }*/

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

        //outsideWall.SetActive(true);
    }

    void SettingStartAndGoal()
    {
        //List<GameObject> rooms = new List<GameObject>();
        //GameObject room;

        List<int> posX = new List<int>();
        List<int> posY = new List<int>();

        for (int i = 0; i < mapVerLength; ++i)
        {
            for (int j = 0; j < mapHoriLength; ++j)
            {
                //Debug.Log(mapEreas[j, i]);
                if (roomTileMap.HasTile(new Vector3Int(j, i, 0)))
                {
                    posX.Add(j);
                    posY.Add(i);
                }
            }
        }
        
        int random = Random.Range(0, posX.Count);
        
        //room = Instantiate(startRoomPrefab, rooms[random].transform.position, Quaternion.identity);
        //removeObj = rooms[random];
        //Destroy(rooms[random]);
        //rooms[random] = room;
        //playerStartPos = rooms[random].transform.position;
        //rooms.RemoveAt(random);
        roomTileMap.SetTile(new Vector3Int(posX[random], posY[random], 0), startRoom);
        miniMapTileMap.SetTile(new Vector3Int(posX[random], posY[random], 0), startRoom);

        playerStartPos = roomTileMap.GetCellCenterWorld(new Vector3Int(posX[random], posY[random], 0));

        posX.RemoveAt(random);
        posY.RemoveAt(random);


        //random = Random.Range(0, rooms.Count);

        //room = Instantiate(goalRoomPrefab, rooms[random].transform.position, Quaternion.identity);
        //removeObj = rooms[random];
        //Destroy(rooms[random]);
        //rooms[random] = room;

        random = Random.Range(0, posX.Count);
        
        roomTileMap.SetTile(new Vector3Int(posX[random], posY[random], 0), goalRoom);
        miniMapTileMap.SetTile(new Vector3Int(posX[random], posY[random], 0), goalRoom);
    }
}
