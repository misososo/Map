using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickRoom : Room
{
    [SerializeField] GameObject gimmickObjPrefab;
    GameObject gimmickObj;

    public override void ArrangementObject()
    {
        gimmickObj = Instantiate(gimmickObjPrefab, transform.position, Quaternion.identity);
    }

    public override void EnebleObject()
    {
        gimmickObj.SetActive(true);
    }

    public override void DisableObject()
    {
        gimmickObj.SetActive(false);
    }
}
