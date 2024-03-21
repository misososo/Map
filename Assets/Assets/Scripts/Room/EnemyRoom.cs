using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : Room
{
    [SerializeField] List<Transform> enemyAppearPoint;
    
    [SerializeField] int minAppearNum;
    [SerializeField] int maxAppearNum;

    [SerializeField] List<Enemy> enemys;
    List<Enemy> roomEnemys = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ArrangementObject()
    {
        Debug.Log("Enemy");
        //敵出現数
        int appearNum = Random.Range(minAppearNum, maxAppearNum + 1);
        //出現ポイントを決める番号
        int appearPointIndex;
        //出現する敵を決める番号
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
    }

    public override void EnebleObject()
    {
        int num = roomEnemys.Count;

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
        int num = roomEnemys.Count;

        for(int i = 0; i < roomEnemys.Count; ++i)
        {           
            roomEnemys[i].gameObject.SetActive(false);
        }

        num = dropSkills.Count;

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
    }
}
