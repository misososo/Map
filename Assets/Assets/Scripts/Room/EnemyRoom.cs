using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyRoom : Room
{
    [SerializeField] List<Transform> enemyAppearPoint;
    
    [SerializeField] int minAppearNum;
    [SerializeField] int maxAppearNum;

    [SerializeField] List<Enemy> enemys;
    List<Enemy> roomEnemys = new List<Enemy>();

    Tilemap tileMap;
    [SerializeField] Tile wall;
    Vector3Int[] setWallPos = { new Vector3Int(1, 2, 0), new Vector3Int(1, -4, 0), new Vector3Int(-2, -1, 0), new Vector3Int(4, -1, 0) };
    
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.Find("ScreenTilemap").GetComponent<Tilemap>();
    }

    public override void ArrangementObject()
    {
        //�G�o����
        int appearNum = Random.Range(minAppearNum, maxAppearNum + 1);
        //�o���|�C���g�����߂�ԍ�
        int appearPointIndex;
        //�o������G�����߂�ԍ�
        int appearEnemyIndex;

        for (int i = 0; i < appearNum; ++i)
        {
            appearPointIndex = Random.Range(0, enemyAppearPoint.Count);
            appearEnemyIndex = Random.Range(0, enemys.Count);

            roomEnemys.Add(Instantiate(enemys[appearEnemyIndex], enemyAppearPoint[appearPointIndex].position, Quaternion.identity));
            
            enemyAppearPoint.RemoveAt(appearPointIndex);
        }

        for(int i = 0; i < roomEnemys.Count; ++i)
        {
            roomEnemys[i].SetId(i);
            roomEnemys[i].RemoveEnemyListCallBack(RemoveRoomEnemys);
        }

        for(int i = 0; i < setWallPos.Length; ++i)
        {
            tileMap.SetTile(setWallPos[i], wall);
        }
    }

    public override void EnebleObject()
    {
        for (int i = 0; i < roomEnemys.Count; ++i)
        {
            roomEnemys[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(true);
        }
    }

    public override void DisableObject()
    {
        for(int i = 0; i < roomEnemys.Count; ++i)
        {           
            roomEnemys[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(false);
        }
    }

    void RemoveRoomEnemys(int index)
    {
        for(int i = 0; i < roomEnemys.Count; ++i)
        {
            if(index == roomEnemys[i].GetId())
            {
                roomEnemys.RemoveAt(i);
            }
        }    
        
        if(roomEnemys.Count == 0)
        {
            for(int i = 0; i < setWallPos.Length; ++i)
            {
                tileMap.SetTile(setWallPos[i], null);
            }
        }
    }
}
