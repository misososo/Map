using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Collider2D playerCollider;

    private Vector3 inputL;
    private Vector3 inputR;

    private Vector3 move;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] GameObject[] lifeBowls = new GameObject[6];
    [SerializeField] GameObject[] lifes = new GameObject[6];

    [SerializeField] GameObject bom;
    [SerializeField] int bomNum;

    [SerializeField] float invincibleTime;
    
    [SerializeField] PlayerCamera pc;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletShot;
    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] float shotSpan;
    float firstShotSpan;

    [SerializeField] SkillSlot SkillSlot;

    GameObject hitObj;
    
    [SerializeField] Text touchObjName;
    [SerializeField] Text touchObjInfo;
    [SerializeField] Text bomNumText;

    bool isDie = false;

    bool isHitEnemy = false;

    SpriteRenderer sr;

    [SerializeField] float flushSpan = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        firstShotSpan = shotSpan;

        touchObjName.text = "";
        touchObjInfo.text = "";
        bomNumText.text = "x" + bomNum;

        for (int i = 0; i < maxHp; ++i)
        {
            lifeBowls[i].SetActive(true);
        }

        for(int i = 0; i < hp; ++i)
        {
            lifes[i].SetActive(true);
        }

        StartCoroutine(DisableCollider());
    }

    private void FixedUpdate()
    {
        if (inputL!= Vector3.zero && !isDie)
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.GetIsGameCrear())
        {
            playerCollider.enabled = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(GameManager.I.GameCrear());
            return;
        }

        if(isHitEnemy)
        {
            isHitEnemy = false;

            hp--;
            lifes[hp].SetActive(false);
            GameManager.I.PlaySE((int)GameManager.SE.damage, transform.position);
           
            if (hp == 0)
            {
                Die();
            }

            StartCoroutine(InvincibleMode(invincibleTime));
        }
       
        if (isDie) return;

        if(inputL == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            SettingMoveAmount();
            transform.rotation = Quaternion.LookRotation(Vector3.forward, inputL);
        }
        
        if(inputR != Vector3.zero)
        {
            shotSpan -= Time.deltaTime;

            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, inputR);

            if(shotSpan <= 0)
            {
                shotSpan = firstShotSpan;

                bullet = Instantiate(bulletPrefab, bulletShot.transform.position, Quaternion.identity);
                bullet.SetDir(gun.transform.up);
                bullet = null;
                GameManager.I.PlaySE((int)GameManager.SE.shot, transform.position);
            }
            
        }
        else
        {
            shotSpan = 0;

            gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skill"))
        {
            hitObj = collision.gameObject;
            Debug.Log(hitObj);

            Skill skill = hitObj.GetComponent<Skill>();

            if (!skill) return;

            touchObjName.text = skill.GetName();
            touchObjInfo.text = skill.GetInfo();
        }
        else if (collision.CompareTag("Goal"))
        {
            hitObj = collision.gameObject;
            Debug.Log(hitObj);
        }

        if (collision.CompareTag("CameraPoint"))
        {
            pc.SetTarget(collision.gameObject);

            Room room = collision.GetComponent<Room>();

            if(!room)
                return;
       
            room.RemoveSprite();
            //GameManager.I.SetRoomId(room.GetId());
            
        }

        

        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("EnemyAttack") || collision.CompareTag("Explosion") || collision.CompareTag("Gimmick"))
        {
            if (hp <= 0) return;
            isHitEnemy = true;
            
        }

        if(collision.CompareTag("Life"))
        {
            if(hp == maxHp)return;

            lifes[hp].SetActive(true);

            hp++;

            GameManager.I.PlaySE((int)GameManager.SE.heal, transform.position);

            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("Bom"))
        {
            bomNum++;
            bomNumText.text = "x" + bomNum;

            GameManager.I.PlaySE((int)GameManager.SE.heal, transform.position);

            Destroy(collision.gameObject);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitObj = null;
        
        touchObjName.text = "";
        touchObjInfo.text = "";
    }

    public void CheckInputLeftStick(InputAction.CallbackContext context)
    {
        inputL.x = context.ReadValue<Vector2>().x;
        inputL.y = context.ReadValue<Vector2>().y;
    }

    public void CheckInputRightStick(InputAction.CallbackContext context)
    {
        inputR.x = context.ReadValue<Vector2>().x;
        inputR.y = context.ReadValue<Vector2>().y;
    }

    public void CheckInputSouthButton(InputAction.CallbackContext context)
    {
        if (!hitObj) return;

        if (context.performed && hitObj.CompareTag("Skill"))
        {
            Skill skill = hitObj.GetComponent<Skill>();

            if (!skill) return;
            if (SkillSlot.GetHaveSkill()) return;

            skill.SetId(-1);
            EquipmentSkill(skill);

            StartCoroutine(DisableCollider());
        }
        else if (context.performed && hitObj.CompareTag("Goal"))
        {
            StartCoroutine(GameManager.I.ChangeScene(GameManager.I.GetNextScene()));
        }
    }

    public void CheckInputEastButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DropSkill();
        }
    }

    public void CheckInputLB(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PutBom();
        }
    }

    void EquipmentSkill(Skill skill)
    {
        for (int i = 0; i < GameManager.I.GetSkillNum(); ++i)
        {
            if (skill.GetName() == GameManager.I.GetSkill(i).GetName())
            {
                SkillSlot.EntryHaveSkill(GameManager.I.GetSkill(i));
                SkillSlot.GetHaveSkill().EnableSkill(ref bulletPrefab);
                //Debug.Log(bulletPrefab.GetReflect());
                GameManager.I.PlaySE((int)GameManager.SE.heal, transform.position);

                Destroy(skill.gameObject);

                break;
            }
        }
    }

    void DropSkill()
    {
        for (int i = 0; i < GameManager.I.GetSkillNum(); ++i)
        {
            if (SkillSlot.GetHaveSkill() && SkillSlot.GetHaveSkill().GetName() == GameManager.I.GetSkill(i).GetName())
            {
                Instantiate(GameManager.I.GetSkill(i), transform.position, Quaternion.identity);
                SkillSlot.RemoveHaveSkill(ref bulletPrefab);
            }
        }
    }

    void SettingMoveAmount()
    {
        if (inputL.x == 0) return;

        move = inputL * moveSpeed;
    }

    void Move()
    {
        rb.velocity = new Vector3(move.x, move.y, 0);
    }



    IEnumerator InvincibleMode(float time)
    {
        playerCollider.enabled = false;

        yield return Flush(time, flushSpan);

        playerCollider.enabled = true;
    }

    IEnumerator Flush(float time, float span)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(span);
            time -= span;

            sr.enabled = false;
            gun.SetActive(false);

            yield return new WaitForSeconds(span);
            time -= span;

            sr.enabled = true;
            gun.SetActive(true);
        }
    }

    IEnumerator DisableCollider()
    {
        playerCollider.enabled = false;

        yield return null;

        playerCollider.enabled = true;
    }

    void Die()
    {
        isDie = true;
        rb.velocity = Vector2.zero;
        //Instantiate(apPrefab);
        StartCoroutine(GameManager.I.GameOver());
        //StartCoroutine(GameManager.I.ChangeScene("Title"));
    }

    void PutBom()
    {
        if(bomNum <= 0) return;

        bomNum--;
        bomNumText.text = "x" + bomNum;

        Instantiate(bom, transform.position, Quaternion.identity);
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void SetHp(int i)
    {
        hp = i;
    }

    public void SetMaxHp(int i)
    {
        maxHp = i;
    }

    public int GetBomNum()
    {
        return bomNum;
    }

    public void SetBomNum(int i)
    {
        bomNum = i;
    }
}
