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
    }

    // Update is called once per frame
    void Update()
    {
        //move = transform.position;
        //move += (player.transform.position - transform.position).normalized * moveSpeed;
        //transform.position = move;

        move = transform.position;
        move += (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        transform.position = move;
    }


}