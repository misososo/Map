using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] int mapHoriLength;
    [SerializeField] int mapVerLength;

    [SerializeField] GameObject mapEreasParent;
    GameObject[,] mapEreas;

    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject startRoomPrefab;
    [SerializeField] GameObject goalRoomPrefab;
    [SerializeField] GameObject insideWall;
    [SerializeField] GameObject outsideWall;

    [SerializeField] int roomNum = 5;

    GameObject removeObj;
    
    // Start is called before the first frame update
    void Start()
    {
        mapEreas = new GameObject[mapVerLength, mapHoriLength];

        int childIndex = 0;

        for(int i = 0; i < mapVerLength; ++i)
        {
            for(int j = 0; j < mapHoriLength; ++j)
            {
                mapEreas[j, i] = mapEreasParent.transform.GetChild(childIndex).gameObject;
                childIndex++;
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
        
    }

    void CreateRoom(int posX, int posY)
    {
        if(roomNum <= 0)return;
        if(!mapEreas[posX, posY].CompareTag("Wall"))return;

        roomNum--;
        GameObject room = Instantiate(roomPrefab, mapEreas[posX, posY].transform.position, Quaternion.identity);
        //removeObj = mapEreas[posX, posY];
        Destroy(mapEreas[posX, posY]);
        mapEreas[posX, posY] = room;

        List<int> createDirX = new List<int>();
        List<int> createDirY = new List<int>();

        if(CheckCreateRoom(posX + 1, posY))
        {
            createDirX.Add(posX + 1);
            createDirY.Add(posY);
        }

        if(CheckCreateRoom(posX - 1, posY))
        {
            createDirX.Add(posX - 1);
            createDirY.Add(posY);
        }

        if(CheckCreateRoom(posX, posY + 1))
        {
            createDirX.Add(posX);
            createDirY.Add(posY + 1);
        }

        if(CheckCreateRoom(posX, posY - 1))
        {
            createDirX.Add(posX);
            createDirY.Add(posY - 1);
        }

        while(createDirX.Count > 0)
        {
            int random = Random.Range(0, createDirX.Count);
            
            for(int i = 0; i < createDirX.Count; ++i)
            {
                if(i == random)
                {
                    CreateRoom(createDirX[i], createDirY[i]);
                    createDirX.RemoveAt(i);
                    createDirY.RemoveAt(i);
                    break;
                }
            }
        }
        
    }

    bool CheckCreateRoom(int posX, int posY)
    {
        if(posX >= mapHoriLength || posX < 0)return false;
        if(posY >= mapVerLength || posY < 0)return false;
        

        return true;
    }

    bool CheckCreateWall(int posX, int posY)
    {
        if (mapEreas[posX + 1, posY    ].CompareTag("Room") &&
            mapEreas[posX - 1, posY    ].CompareTag("Room") &&
            mapEreas[posX,     posY + 1].CompareTag("Room") &&
            mapEreas[posX,     posY - 1].CompareTag("Room") &&
            mapEreas[posX + 1, posY + 1].CompareTag("Room") &&
            mapEreas[posX + 1, posY - 1].CompareTag("Room") &&
            mapEreas[posX - 1, posY + 1].CompareTag("Room") &&
            mapEreas[posX - 1, posY - 1].CompareTag("Room"))
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
        //List<GameObject> createWalls = new List<GameObject>();
        List<int> posX = new List<int>();
        List<int> posY = new List<int>();

        GameObject wall;

        for (int i = 1; i < mapVerLength - 1; ++i)
        {
            for (int j = 1; j < mapHoriLength - 1; ++j)
            {
                if (!mapEreas[j, i].CompareTag("Room")) continue;
                
                if(CheckCreateWall(j, i))
                {
                    posX.Add(j);
                    posY.Add(i);
                }
            }
        }
        
        for(int i = 0; i < posX.Count; ++i)
        {
            wall = Instantiate(insideWall, mapEreas[posX[i], posY[i]].transform.position, Quaternion.identity);
            //removeObj = mapEreas[posX[i], posY[i]];
            Destroy(mapEreas[posX[i], posY[i]]);
            mapEreas[posX[i], posY[i]] = wall;
        }

        //outsideWall.SetActive(true);
    }

    void SettingStartAndGoal()
    {
        List<GameObject> rooms = new List<GameObject>();
        GameObject room;

        for (int i = 0; i < mapVerLength; ++i)
        {
            for (int j = 0; j < mapHoriLength; ++j)
            {
                Debug.Log(mapEreas[j, i]);
                if (mapEreas[j, i].CompareTag("Room"))
                {
                    rooms.Add(mapEreas[j, i]);
                }
            }
        }

        int random = Random.Range(0, rooms.Count);

        room = Instantiate(startRoomPrefab, rooms[random].transform.position, Quaternion.identity);
        removeObj = rooms[random];
        Destroy(removeObj);
        rooms[random] = room;
        rooms.RemoveAt(random);

        random = Random.Range(0, rooms.Count);

        room = Instantiate(goalRoomPrefab, rooms[random].transform.position, Quaternion.identity);
        removeObj = rooms[random];
        Destroy(removeObj);
        rooms[random] = room;
    }
}
