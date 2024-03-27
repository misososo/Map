using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : MonoBehaviour
{
    int mode = 0;
    public Vector2 speed;
    Rigidbody2D rb;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
       rb= GetComponent<Rigidbody2D>();
        //speed = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        switch(mode)
        {
            case 0:
                time += Time.deltaTime;
                if(time>2)
                {
                    time = 0;
                    mode = 1;
                }

                rb.velocity = speed;
                break;

            case 1:
                time += Time.deltaTime;
                if(time > 2)
                {
                    time = 0;
                    mode = 0;
                }
                rb.velocity = -speed;
                break;

        }
    }
}
