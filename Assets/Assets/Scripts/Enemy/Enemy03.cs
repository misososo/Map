using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03 : Enemy
{
    float dirX;
    float dirY;

    Vector3 reflect;

    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    
    [SerializeField] float minShotSpan;
    [SerializeField] float maxShotSpan;
    float shotSpan;
    Vector3[] shotDir = {new Vector3(1, 0, 0),
                         new Vector3(1, 1, 0),
                         new Vector3(0, 1, 0),
                         new Vector3(-1, 1, 0),
                         new Vector3(-1, 0, 0),
                         new Vector3(-1, -1, 0),
                         new Vector3(0, -1, 0),
                         new Vector3(1, -1, 0)};

    void Start()
    {
        SetStatus();        
        rb = GetComponent<Rigidbody2D>();
        dirX = Random.Range(-1.0f, 1.0f);
        dirY = Random.Range(-1.0f, 1.0f);
        move = new Vector3(dirX, dirY, 0).normalized * moveSpeed;
        rb.velocity = move;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, move);
        shotSpan = Random.Range(minShotSpan, maxShotSpan);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        shotSpan -= Time.deltaTime;

        if(shotSpan <= 0)
        {
            shotSpan = Random.Range(minShotSpan, maxShotSpan);

            for (int i = 0; i < shotDir.Length; ++i)
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.SetDir(shotDir[i].normalized);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void ReflectWall(Collision2D collision)
    {
        reflect = Vector3.Reflect(transform.up, collision.contacts[0].normal).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, reflect);
        rb.velocity = transform.up * moveSpeed;
    }
}
