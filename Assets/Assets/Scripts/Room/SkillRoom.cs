using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRoom : Room
{
    //[SerializeField] List<Transform> skillAppearPoint;
    //[SerializeField] int appearNum;

    //List<Skill> roomSkills = new List<Skill>();
    //int roomSkillsNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //protected override void Update()
    //{
    //    base.Update();

    //    //if(roomSkillsNum != roomSkills.Count && roomSkillsNum != 0)
    //    //{
    //    //    roomSkillsNum = roomSkills.Count;
    //    //    Debug.Log("EEEE");
    //    //}
    //}

    public override void ArrangementObject()
    {
        int appearSkill = Random.Range(0, GameManager.I.GetSkillNum());
        Instantiate(GameManager.I.GetSkill(appearSkill), transform.position, Quaternion.identity);

        //for(int i = 0; i < appearNum; ++i)
        //{
        //    roomSkills.Add(Instantiate(GameManager.I.GetSkill(appearSkill), skillAppearPoint[i].position, Quaternion.identity));
        //    roomSkills[roomSkills.Count - 1].RemoveRoomSkillsCallBack(RemoveRoomSkills);
        //}

        //roomSkillsNum = roomSkills.Count;
    }

    //void RemoveRoomSkills()
    //{
        
    //    for(int i = 0; i < roomSkills.Count; ++i)
    //    {
    //        if(roomSkills[i])
    //        {
    //            Debug.Log("FFFF");
    //            Destroy(roomSkills[i]);
    //        }
    //    }
    //        roomSkills.Clear();
    //}
}
