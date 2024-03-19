using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject gun;
    Vector3 pos;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] PlayerCamera pc;
    Vector3 pcOldPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = gun.transform.up;
        pcOldPos = pc.transform.position;

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = pos * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(pcOldPos != pc.transform.position)
        {
            pcOldPos = pc.transform.position;

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}