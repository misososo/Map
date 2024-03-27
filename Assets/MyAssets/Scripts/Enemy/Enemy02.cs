using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : Enemy
{
    GameObject player;
    [SerializeField] float maxWarpSpan;
    [SerializeField] float minWarpSpan;
    float warpSpan;
    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        warpSpan = Random.Range(minWarpSpan, maxWarpSpan);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        warpSpan -= Time.deltaTime;

        if(warpSpan <= 0)
        {
            warpSpan = Random.Range(minWarpSpan, maxWarpSpan);
            transform.position = player.transform.position;
        }
    }

    void Warp()
    {
        
    }
}
