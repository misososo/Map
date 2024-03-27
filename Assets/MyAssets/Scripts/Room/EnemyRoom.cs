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

    [SerializeField, Range(0.0f, 1.0f)] float skillRewardProb;
    [SerializeField, Range(0.0f, 1.0f)] float itemRewardProb;
    
    public override void ArrangementObject()
    {
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

        for(int i = 0; i < setWallPos.Length; ++i)
        {
            screenTileMap.SetTile(setWallPos[i], wall);
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
            CheckNextRoom();

            float appearReward = Random.value;

            if(appearReward <= skillRewardProb)
            {
                //int appearSkill = Random.Range(0, GameManager.I.GetSkillNum());
                //Instantiate(GameManager.I.GetSkill(appearSkill), transform.position, Quaternion.identity);
            }
            else if(skillRewardProb < appearReward && appearReward <= itemRewardProb)
            {
                int appearItem = Random.Range(0, GameManager.I.GetItemNum());
                Instantiate(GameManager.I.GetItem(appearItem), transform.position, Quaternion.identity);
            }
            
        }

        
    }
}
