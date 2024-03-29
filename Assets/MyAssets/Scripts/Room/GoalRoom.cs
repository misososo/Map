using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRoom : Room
{
    [SerializeField] GameObject goalObjPrefab;
    GameObject goalObj;
    [SerializeField] float objPosZ;

    public override void ArrangementObject()
    {
        Vector3 pos = transform.position;
        pos.z = objPosZ;
        goalObj = Instantiate(goalObjPrefab, pos, Quaternion.identity);
    }

    public override void EnebleObject()
    {
        goalObj.SetActive(true);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(true);
        }
    }

    public override void DisableObject()
    {
        goalObj.SetActive(false);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(false);
        }
    }
}
