using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room
{
    // Start is called before the first frame update
    void Start()
    {
        ArrangementObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ArrangementObject()
    {
        Debug.Log("Start");
    }
}
