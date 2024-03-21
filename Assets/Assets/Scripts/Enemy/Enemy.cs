using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    protected int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStatus()
    {

    }

    public void SetId(int i)
    {
        id = i;
    }

    private void OnDestroy()
    {
        //removeRoomEnemys(id);
    }

    //コールバック関数
    public delegate void CallBack(int i);

    CallBack removeRoomEnemys;

    public void RemoveEnemyListCallBack(CallBack callBack)
    {
        removeRoomEnemys = callBack;
    }
}
