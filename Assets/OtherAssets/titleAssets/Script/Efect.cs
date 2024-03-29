using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efect : MonoBehaviour
{
    public int Count;
    public GameObject Mozi;
    public GameObject EXplo;

    void Start()
    {
        Count = 0;
    }

   
    void Update()
    {
        if(Count>=20)
        {

            Instantiate(EXplo.gameObject,Mozi.transform.position,Mozi.transform.rotation);

            

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Count += 1;

        Debug.Log(Count);

    }
}
