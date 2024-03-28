using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRoom : Room
{
    [SerializeField] GameObject stone;
    [SerializeField] bool isStone;

    public override void ArrangementObject()
    {
        int appearSkill = Random.Range(0, GameManager.I.GetSkillNum());
        Instantiate(GameManager.I.GetSkill(appearSkill), transform.position, Quaternion.identity);

        if(isStone)
        {
            Instantiate(stone, transform.position, Quaternion.identity);
        }
    }
}
