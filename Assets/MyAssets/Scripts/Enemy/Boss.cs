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
    int attackRandom;

    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] int shotBulletNum;
    [SerializeField] float shotBulletSpan;

    [SerializeField] Bullet beamPrefab;
    Bullet beam;
    [SerializeField] int shotBeamNum;
    [SerializeField] float shotBeamSpan;

    bool isAwaken = false;
    [SerializeField] int awakenHp;

    Vector3 firstPos;

    [SerializeField] Collider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();

        attackSpan = firstAttackSpan;

        firstPos = transform.position;
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

            attackRandom = Random.Range(0, 2);

            if(attackRandom == 0)
            {
                Rush();
            }
            else if(attackRandom == 1)
            {
                StartCoroutine(ShotBeam());
            } 
            else
            {
                
            }
        }
    }

    void Rush()
    {
        move = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = move;       
    }

    IEnumerator ShotBullet()
    {
        for(int i = 0; i < shotBulletNum; ++i)
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            dir.x = Random.Range(-1.0f, 1.0f);
            dir.y = Random.Range(-1.0f, 1.0f);

            bullet.SetDir(dir.normalized);

            yield return new WaitForSeconds(shotBulletSpan);
        }

        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    IEnumerator ShotBeam()
    {
        for(int i = 0; i < shotBeamNum; ++i)
        {
            beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);

            dir = (player.transform.position - transform.position).normalized;


            beam.SetDir(dir);

            yield return new WaitForSeconds(shotBeamSpan);
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

    IEnumerator EnableCollider()
    {
        enemyCollider.enabled = false;

        yield return null;

        enemyCollider.enabled = true;
    }
}
