using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shutugen : MonoBehaviour
{
    public float timeMax;
    float timeNow = 0;
    BoxCollider2D bc;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = timeNow < timeMax / 2 ;
        timeNow += Time.deltaTime;
        timeNow %= timeMax;
        bc.enabled = flag;
        sr.enabled = flag;
    }
}
