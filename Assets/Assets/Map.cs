using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] int mapHoriLength;
    [SerializeField] int mapVerLength;

    [SerializeField] GameObject roomCreatePoints;
    GameObject[,] roomCreatePoint;

    [SerializeField] GameObject roomPrefab;
    List<GameObject> rooms = new List<GameObject>();   
    [SerializeField] GameObject startRoomPrefab;
    [SerializeField] GameObject goalRoomPrefab;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject outerWall;

    [SerializeField] int roomNum = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        roomCreatePoint = new GameObject[mapVerLength, mapHoriLength];

        int childIndex = 0;

        for(int i = 0; i < mapVerLength; ++i)
        {
            for(int j = 0; j < mapHoriLength; ++j)
            {
                roomCreatePoint[j, i] = roomCreatePoints.transform.GetChild(childIndex).gameObject;
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

        for (int i = 0; i < mapVerLength; ++i)
        {
            for(int j = 0; j < mapHoriLength; ++j)
            {
                if (i == randomY && j == randomX)
                {               
                    CreateRoom(j, i);
                    break;
                }
            }           
        }

        RemoveRoom();
        SettingStartAndGoal();
        outerWall.SetActive(true);
    }

    void CreateRoom(int posX, int posY)
    {
        if(roomNum <= 0)return;
        if(!roomCreatePoint[posX, posY])return;

        roomNum--;
        GameObject room = Instantiate(roomPrefab, roomCreatePoint[posX, posY].transform.position, Quaternion.identity);
        rooms.Add(room);

        Destroy(roomCreatePoint[posX, posY]);
        roomCreatePoint[posX, posY] = null;

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

    void RemoveRoom()
    {
        List<GameObject> removeRooms = new List<GameObject>();
        Vector3 pos;

        for(int i = 0; i < rooms.Count; ++i)
        {
            pos = rooms[i].transform.position;

            if(Physics2D.Raycast(new Vector3(pos.x + 1, pos.y, 0),         Vector3.right,    0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x - 1, pos.y, 0),         Vector3.left,     0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x, pos.y + 1, 0),         Vector3.up,       0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x, pos.y - 1, 0),         Vector3.down,     0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x + 1, pos.y + 1, 0), new Vector3(1, 1, 0), 0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x + 1, pos.y - 1, 0), new Vector3(1, 1, 0), 0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x - 1, pos.y + 1, 0), new Vector3(1, 1, 0), 0.1f) &&
               Physics2D.Raycast(new Vector3(pos.x - 1, pos.y - 1, 0), new Vector3(1, 1, 0), 0.1f))
            {
                Debug.Log("AAA");
                removeRooms.Add(rooms[i]);           
                rooms.RemoveAt(i);
                i--;
            }
            
        }

        for(int i = 0; i < removeRooms.Count; ++i)
        {
            Destroy(removeRooms[i]);
            removeRooms[i] = Instantiate(wall, removeRooms[i].transform.position, Quaternion.identity);
        }
    }

    void SettingStartAndGoal()
    {
        List<GameObject> roomsCopy = new List<GameObject>(rooms);
        int random = Random.Range(0, roomsCopy.Count);

        Destroy(roomsCopy[random]);
        roomsCopy[random] = Instantiate(startRoomPrefab, roomsCopy[random].transform.position, Quaternion.identity);
        roomsCopy.RemoveAt(random);

        random = Random.Range(0, roomsCopy.Count);

        Destroy(roomsCopy[random]);
        roomsCopy[random] = Instantiate(goalRoomPrefab, roomsCopy[random].transform.position, Quaternion.identity);
        roomsCopy.RemoveAt(random);
    }
}
