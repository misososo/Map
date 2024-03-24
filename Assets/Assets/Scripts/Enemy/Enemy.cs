using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    new string name;
    protected int hp;
    protected float moveSpeed;

    protected int id;

    protected Vector3 move;

    protected Rigidbody2D rb;

    protected virtual void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            hp--;
            //Debug.Log(hp);
        }
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
        //Debug.Log("dead");
        removeRoomEnemys(id);
    }

    //コールバック関数
    public delegate void CallBackVoid(int i);

    CallBackVoid removeRoomEnemys;

    public void RemoveEnemyListCallBack(CallBackVoid callBack)
    {
        removeRoomEnemys = callBack;
    }
}
