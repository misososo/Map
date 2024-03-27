using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionSkill : Skill
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
        bullet.PlusDivisionNum();
        Debug.Log("Division");
    }

    public override void DisableSkill(ref Bullet bullet)
    {
        bullet.MinusDivisionNum();
    }
}
