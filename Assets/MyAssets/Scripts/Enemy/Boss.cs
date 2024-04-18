using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Player player;
      
    [SerializeField] 
    float maxAttackSpan;      //攻撃頻度の最大値
    [SerializeField] 
    float minAttackSpan;      //攻撃頻度の最小値
    [SerializeField] 
    float firstAttackSpan;    //初回攻撃までの待機時間
    float attackSpan;         //攻撃頻度
    bool isAttackNow = false; //攻撃中かどうかの判定
    int attackRandom;
    List<int> attackPattern = new List<int>(); //攻撃パターン一覧
    enum Attack
    {
        rush,//突進
        beam,//ビーム
        laser//レーザー
    }
    int attackNum = 3; //攻撃パターン数

    [SerializeField] 
    Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] 
    int shotBulletNum;   //弾丸連射数
    [SerializeField] 
    float shotBulletSpan;//弾丸連射速度

    [SerializeField] 
    Bullet beamPrefab;
    Bullet beam;
    [SerializeField] 
    int shotBeamNum;   //ビーム連射数
    [SerializeField] 
    float shotBeamSpan;//ビーム連射数

    [SerializeField] 
    Laser laserPrefab; 
    Laser laser;
    [SerializeField] 
    GameObject preLaserPrefab;//発射準備中レーザーのプレハブ
    GameObject preLaser;　　　//発射準備中レーザー
    [SerializeField] 
    float preTime;            //レーザー発射準備時間
    [SerializeField] 
    float laserLifeTime;      //レーザー発射時間


    bool isAwaken = false;        //覚醒したかどうか
    [SerializeField] int awakenHp;//覚醒状態になる残りhp量

    Vector3 firstPos; //初期地点

    [SerializeField] 
    float goCenterTime;//マップの中心に向かって動く時間

    [SerializeField] 
    Collider2D[] attackCollider;//攻撃用コライダー

    Vector3 rushDir;//突進方向
    [SerializeField] 
    float rushSpeed;//突進スピード

    [SerializeField] GameObject leftArm;  
    [SerializeField] GameObject rightArm; 
    [SerializeField] Transform lArmAtkPos;//攻撃時の左腕のポジション
    [SerializeField] Transform rArmAtkPos;//攻撃時の右腕のポジション
    [SerializeField] float armMoveSpeed;  //腕の移動スピード
    [SerializeField] float armRotSpeed;   //腕の回転スピード
    Vector3 lArmMoveDir; //左腕の移動方向
    Vector3 rArmMoveDir; //右腕の移動方向
    Vector3 lArmAngle;   //左腕の角度
    Vector3 rArmAngle;   //右腕の角度

    [SerializeField] 
    GameObject hitEffectPrefab;//死亡時のエフェクト

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

            if(attackPattern[attackRandom] == (int)Attack.rush)
            {
                StartCoroutine(Rush());
            }
            else if(attackPattern[attackRandom] == (int)Attack.beam)
            {
                StartCoroutine(ShotBeam());
            }
            else if (attackPattern[attackRandom] == (int)Attack.laser)
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
        Vector2 dir;

        for (int i = 0; i < shotBulletNum; ++i)
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
        Vector2 dir;

        for (int i = 0; i < shotBeamNum; ++i)
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
