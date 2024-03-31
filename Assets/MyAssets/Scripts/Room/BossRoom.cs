using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] Enemy bossPrefab;
    Enemy boss;

    public override void ArrangementObject()
    {
        GameManager.I.MusicChange((int)GameManager.Music.boss);
        boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        boss.RemoveEnemyListCallBack(RemoveRoomEnemys);

        for(int i = 0; i < setWallPos.Length; ++i)
        {
            screenTileMap.SetTile(setWallPos[i], wall);
        }
    }

    void RemoveRoomEnemys(int index)
    {      
        CheckNextRoom();
        GameManager.I.MusicStop();
    }
}
