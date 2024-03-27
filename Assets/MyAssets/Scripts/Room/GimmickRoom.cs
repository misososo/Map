using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickRoom : Room
{
    [SerializeField] GameObject[] gimmickObjPrefab;
    GameObject gimmickObj;

    [SerializeField, Range(0.0f, 1.0f)] float skillRewardProb;
    [SerializeField, Range(0.0f, 1.0f)] float itemRewardProb;

    bool isPutSkill = false;

    protected override void Update()
    {
        base.Update();

        if (dropSkills.Count > 0)
        {
            isPutSkill = true;
        }

        if (isPutSkill && dropSkills.Count <= 0 && gimmickObj)
        {
            Debug.Log("WWWW");
            Destroy(gimmickObj);
            gimmickObj = null;
        }
    }

    public override void ArrangementObject()
    {
        int select = Random.Range(0, gimmickObjPrefab.Length);
        gimmickObj = Instantiate(gimmickObjPrefab[select], transform.position, Quaternion.identity);

        float appearReward = Random.value;

        if (appearReward <= skillRewardProb)
        {
            int appearSkill = Random.Range(0, GameManager.I.GetSkillNum());
            Instantiate(GameManager.I.GetSkill(appearSkill), transform.position, Quaternion.identity);
        }
        else if (skillRewardProb < appearReward && appearReward <= itemRewardProb)
        {
            //int appearItem = Random.Range(0, GameManager.I.GetItemNum());
            //Instantiate(GameManager.I.GetItem(appearItem), transform.position, Quaternion.identity);
        }
    }

    public override void EnebleObject()
    {
        if (!gimmickObj) return;

        gimmickObj.SetActive(true);
    }

    public override void DisableObject()
    {
        if (!gimmickObj) return;

        gimmickObj.SetActive(false);
    }
}
