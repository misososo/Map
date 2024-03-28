using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04 : Enemy
{
    Vector3 dir;
    [SerializeField] float firstTurnSpan;
    [SerializeField] float maxTurnSpan;
    [SerializeField] float minTurnSpan;
    float turnSpan;
    
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        
        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = Random.Range(-1.0f, 1.0f);

        rb.velocity = dir.normalized * moveSpeed;

        turnSpan = Random.Range(minTurnSpan, maxTurnSpan);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ScreenCollider"))
        {
            rb.velocity = Vector2.zero;

            StartCoroutine(Turn());
        }
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(turnSpan);

        turnSpan = Random.Range(minTurnSpan, maxTurnSpan);

        move = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = move;
    }
}
