using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Enemy
{
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = move;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //move = transform.position;
        //move += (player.transform.position - transform.position).normalized * moveSpeed;
        //transform.position = move;

        base.Update();

        move = transform.position;
        move = (player.transform.position - transform.position).normalized * moveSpeed;
        //transform.position = move;
    }


}
