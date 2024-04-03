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
    List<int> attackPattern = new List<int>();

    int attackNum = 3;

    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] int shotBulletNum;
    [SerializeField] float shotBulletSpan;

    [SerializeField] Bullet beamPrefab;
    Bullet beam;
    [SerializeField] int shotBeamNum;
    [SerializeField] float shotBeamSpan;

    [SerializeField] Laser laserPrefab;
    Laser laser;
    [SerializeField] GameObject preLaserPrefab;
    GameObject preLaser;
    [SerializeField] float preTime;
    [SerializeField] float laserLifeTime;


    bool isAwaken = false;
    [SerializeField] int awakenHp;

    Vector3 firstPos;

    [SerializeField] float goCenterTime;

    [SerializeField] Collider2D[] attackCollider;

    Vector3 rushDir;
    [SerializeField] float rushSpeed;

    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject rightArm;
    [SerializeField] Transform lArmAtkPos;
    [SerializeField] Transform rArmAtkPos;
    [SerializeField] float armMoveSpeed;
    [SerializeField] float armRotSpeed;
    Vector3 lArmMoveDir;
    Vector3 rArmMoveDir;
    Vector3 lArmAngle;
    Vector3 rArmAngle;



    [SerializeField] GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();

        attackSpan = firstAttackSpan;

        firstPos = transform.position;

        for(int i = 0; i < attackNum; ++i)
        {
            attackPattern.Add(i);
        }

        EnableAttackCollider(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (hp <= 0)
        {
            GameManager.I.PlaySE((int)GameManager.SE.bom, transform.position);
        }

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

            if(attackPattern[attackRandom] == 0)
            {
                StartCoroutine(Rush());
            }
            else if(attackPattern[attackRandom] == 1)
            {
                StartCoroutine(ShotBeam());
            } 
            else
            {
                StartCoroutine(ShotLaser());
            }

            int keep = attackPattern[attackRandom];
            attackPattern.RemoveAt(attackRandom);
            attackPattern.Add(keep);
            attackRandom = Random.Range(0, attackPattern.Count - 1);

            Debug.Log(attackPattern[attackRandom]);
        }
    }

    IEnumerator Rush()
    {
        rushDir = (player.transform.position - transform.position).normalized;

        yield return ArmAttackMode(armMoveSpeed);

        EnableAttackCollider(true);
        move = rushDir * rushSpeed;
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
            GameManager.I.PlaySE((int)GameManager.SE.beam, transform.position);

            dir = (player.transform.position - transform.position).normalized;


            beam.SetDir(dir);

            yield return new WaitForSeconds(shotBeamSpan);
        }

        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    IEnumerator ShotLaser()
    {
        preLaser = Instantiate(preLaserPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(preTime);

        Destroy(preLaser.gameObject);
        preLaser = null;

        laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        GameManager.I.PlaySE((int)GameManager.SE.laser, laserLifeTime, transform.position);

        yield return new WaitForSeconds(laserLifeTime);

        Destroy(laser.gameObject);
        laser = null;

        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    IEnumerator GoCenter()
    {
        move = (firstPos - transform.position).normalized;
        rb.velocity = move;

        yield return new WaitForSeconds(goCenterTime);

        rb.velocity = Vector2.zero;
        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.CompareTag("Wall") && isAttackNow)
        {
            rb.velocity = Vector2.zero;
            EnableAttackCollider(false);

            StartCoroutine(ArmNormalMode(armMoveSpeed));
        }
    }

    void EnableAttackCollider(bool b)
    {
        for(int i = 0; i < attackCollider.Length; ++i)
        {
            attackCollider[i].enabled = b;
        }
    }

    IEnumerator ArmAttackMode(float time)
    {
        lArmMoveDir = ((lArmAtkPos.position - leftArm.transform.position) / time);
        rArmMoveDir = ((rArmAtkPos.position - rightArm.transform.position) / time);

        lArmAngle = lArmAtkPos.eulerAngles / time;
        rArmAngle = lArmAtkPos.eulerAngles / time;

        for (int i = 0; i < time; ++i)
        {           
            leftArm.transform.position += lArmMoveDir;
            rightArm.transform.position += rArmMoveDir;

            leftArm.transform.Rotate(lArmAngle);
            rightArm.transform.Rotate(-rArmAngle);

            yield return null;
        }
    }

    IEnumerator ArmNormalMode(float time)
    {
        move = (firstPos - transform.position).normalized;
        rb.velocity = move;

        for (int i = 0; i < time; ++i)
        {
            leftArm.transform.position -= lArmMoveDir;
            rightArm.transform.position -= rArmMoveDir;

            leftArm.transform.Rotate(-lArmAngle);
            rightArm.transform.Rotate(rArmAngle);

            yield return null;
        }

        rb.velocity = Vector2.zero;
        attackSpan = Random.Range(minAttackSpan, maxAttackSpan);
        isAttackNow = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.I.SetIsGameCrear(true);
        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        
    }
}
