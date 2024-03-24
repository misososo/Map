using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ä—í ÉXÉLÉã
public class PenetrationSkill : Skill
{
    // Start is called before the first frame update
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

        bullet.PlusPenetrationNum();
        Debug.Log("Penetration");
    }

    public override void DisableSkill(ref Bullet bullet)
    {
        bullet.MinusPenetrationNum();
    }
}
