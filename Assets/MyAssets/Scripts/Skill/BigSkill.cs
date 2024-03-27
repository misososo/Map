using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ãêëÂâªÉXÉLÉã
public class BigSkill : Skill
{
    void Start()
    {
        SetStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EnableSkill(ref Bullet bullet)
    {
        bullet.PlusProductScale();
        Debug.Log("Big");
    }

    public override void DisableSkill(ref Bullet bullet)
    {
        bullet.MinusProductScale();
    }
}
