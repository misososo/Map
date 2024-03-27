using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room
{
    [SerializeField] GameObject startObjPrefab;
    GameObject startObj;

    public override void ArrangementObject()
    {
        //startObj = Instantiate(startObjPrefab, transform.position, Quaternion.identity);
    }

    public override void EnebleObject()
    {
        //startObj.SetActive(true);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(true);
        }
    }

    public override void DisableObject()
    {
        //startObj.SetActive(false);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            dropSkills[i].gameObject.SetActive(false);
        }
    }
}
