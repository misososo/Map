using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    new string name;   
    int hp;
    protected float moveSpeed;

    protected int id;

    protected Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetStatus()
    {
        name = enemyData.GetName();
        hp = enemyData.GetHp();
        moveSpeed = enemyData.GetMoveSpeed();
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int i)
    {
        id = i;
    }

    private void OnDestroy()
    {
        removeRoomEnemys(id);
    }

    //コールバック関数
    public delegate void CallBack(int i);

    CallBack removeRoomEnemys;

    public void RemoveEnemyListCallBack(CallBack callBack)
    {
        removeRoomEnemys = callBack;
    }
}
