using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Player player;
    Vector3 dir;
    [SerializeField] float maxAttackSpan;
    [SerializeField] float minAttackSpan;
    [SerializeField] float firstAttackSpan;
    float attackSpan;
    bool isAttackNow = false;

    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] int shotNum;
    [SerializeField] float shotSpan;

    bool isAwaken = false;
    [SerializeField] int awakenHp;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();

        attackSpan = firstAttackSpan;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        attackSpan -= Time.deltaTime;

        if(attackSpan <= 0 && !isAttackNow)
        {
            isAttackNow = true;

            if(hp <= awakenHp && !isAwaken)
            {
                isAwaken = true;

                StartCoroutine(ShotBullet());

                return;
            }

            Rush();
        }
    }

    void Rush()
    {
        move = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = move;       
    }

    IEnumerator ShotBullet()
    {
        for(int i = 0; i < shotNum; ++i)
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            dir.x = Random.Range(-1.0f, 1.0f);
            dir.y = Random.Range(-1.0f, 1.0f);

            bullet.SetDir(dir.normalized);

            yield return new WaitForSeconds(shotSpan);
        }

        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
            isAttackNow = false;
        }
    }
}
