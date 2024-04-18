using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Player player;
      
    [SerializeField] 
    float maxAttackSpan;      //�U���p�x�̍ő�l
    [SerializeField] 
    float minAttackSpan;      //�U���p�x�̍ŏ��l
    [SerializeField] 
    float firstAttackSpan;    //����U���܂ł̑ҋ@����
    float attackSpan;         //�U���p�x
    bool isAttackNow = false; //�U�������ǂ����̔���
    int attackRandom;
    List<int> attackPattern = new List<int>(); //�U���p�^�[���ꗗ
    enum Attack
    {
        rush,//�ːi
        beam,//�r�[��
        laser//���[�U�[
    }
    int attackNum = 3; //�U���p�^�[����

    [SerializeField] 
    Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] 
    int shotBulletNum;   //�e�ۘA�ː�
    [SerializeField] 
    float shotBulletSpan;//�e�ۘA�ˑ��x

    [SerializeField] 
    Bullet beamPrefab;
    Bullet beam;
    [SerializeField] 
    int shotBeamNum;   //�r�[���A�ː�
    [SerializeField] 
    float shotBeamSpan;//�r�[���A�ː�

    [SerializeField] 
    Laser laserPrefab; 
    Laser laser;
    [SerializeField] 
    GameObject preLaserPrefab;//���ˏ��������[�U�[�̃v���n�u
    GameObject preLaser;�@�@�@//���ˏ��������[�U�[
    [SerializeField] 
    float preTime;            //���[�U�[���ˏ�������
    [SerializeField] 
    float laserLifeTime;      //���[�U�[���ˎ���


    bool isAwaken = false;        //�o���������ǂ���
    [SerializeField] int awakenHp;//�o����ԂɂȂ�c��hp��

    Vector3 firstPos; //�����n�_

    [SerializeField] 
    float goCenterTime;//�}�b�v�̒��S�Ɍ������ē�������

    [SerializeField] 
    Collider2D[] attackCollider;//�U���p�R���C�_�[

    Vector3 rushDir;//�ːi����
    [SerializeField] 
    float rushSpeed;//�ːi�X�s�[�h

    [SerializeField] GameObject leftArm;  
    [SerializeField] GameObject rightArm; 
    [SerializeField] Transform lArmAtkPos;//�U�����̍��r�̃|�W�V����
    [SerializeField] Transform rArmAtkPos;//�U�����̉E�r�̃|�W�V����
    [SerializeField] float armMoveSpeed;  //�r�̈ړ��X�s�[�h
    [SerializeField] float armRotSpeed;   //�r�̉�]�X�s�[�h
    Vector3 lArmMoveDir; //���r�̈ړ�����
    Vector3 rArmMoveDir; //�E�r�̈ړ�����
    Vector3 lArmAngle;   //���r�̊p�x
    Vector3 rArmAngle;   //�E�r�̊p�x

    [SerializeField] 
    GameObject hitEffectPrefab;//���S���̃G�t�F�N�g

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
