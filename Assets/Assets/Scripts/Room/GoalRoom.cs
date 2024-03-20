using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRoom : Room
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ArrangementObject()
    {
        Debug.Log("Goal");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isArrangement)
            {
                //EnebleObject();
            }
            else
            {
                isArrangement = true;
                ArrangementObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //DisableObject();
        }
    }
}
