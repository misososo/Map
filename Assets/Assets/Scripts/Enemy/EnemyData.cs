using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int hp;
    [SerializeField] float moveSpeed;

    public string GetName()
    {
        return name;
    }

    public int GetHp()
    {
        return hp;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
