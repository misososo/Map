using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : Room
{
    [SerializeField] List<Transform> enemyAppearPoint;
    
    [SerializeField] int minAppearNum;
    [SerializeField] int maxAppearNum;

    List<Enemy> roomEnemys = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            appearEnemyIndex = Random.Range(0, GameManager.I.GetEnemyNum());

            roomEnemys.Add(Instantiate(GameManager.I.GetEnemy(appearEnemyIndex), enemyAppearPoint[appearPointIndex].position, Quaternion.identity));

            enemyAppearPoint.RemoveAt(appearPointIndex);
        }
    }

    public override void EnebleObject()
    {
        for (int i = 0; i < roomEnemys.Count; ++i)
        {
            roomEnemys[i].gameObject.SetActive(true);
        }
    }

    public override void DisableObject()
    {
        for(int i = 0; i < roomEnemys.Count; ++i)
        {
            roomEnemys[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isArrangement)
            {
                EnebleObject();
            }
            else
            {
                isArrangement = true;
                ArrangementObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DisableObject();
        }
    }
}
