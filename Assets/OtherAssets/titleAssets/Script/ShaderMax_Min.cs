using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderMax_Min : MonoBehaviour
{
    private float Plx;//プレイヤーのy座標
    public GameObject gameobj;

    private Material mat;

   // private float A;

    // Start is called before the first frame update
    void Start()
    {
       
        
         mat = new Material(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material=mat;


        // mat= new Material(GetComponent<SpriteRenderer>().material);
        // mat = (GetComponent<SpriteRenderer>().material);
        // mat.SetFloat("_Min", 0);
    }
    private void OnDestroy()
    {



        Destroy(mat);
        mat = null;

    }
    // Update is called once per frame
    void Update()
    {
        Plx = gameobj.transform.position.y;

        
        Plx += 0.01f;
        mat.SetFloat("_Min", Plx);

    }
    



}
