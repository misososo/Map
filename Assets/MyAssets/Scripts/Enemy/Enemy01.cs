using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Enemy
{
    GameObject player;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
        if(move.x > 0)
        {
            sr.flipX = true;
        }
        else if(move.x < 0)
        {
            sr.flipX = false;
        }
    }


}
