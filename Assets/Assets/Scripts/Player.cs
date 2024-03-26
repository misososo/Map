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

    Vector3 dir;

    [SerializeField] PlayerCamera pc;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletShot;
    [SerializeField] Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField] float shotSpan;
    float firstShotSpan;

    //[SerializeField] SkillDatas skillDatas;
    //[SerializeField] List<Skill> allSkillData;
    //[SerializeField] List<Skill> allSkill;
    [SerializeField] SkillSlot SkillSlot;

    bool isGet = false;
    bool isTouthObj = false;

    [SerializeField] Text touchObjName;
    [SerializeField] Text touchObjInfo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstShotSpan = shotSpan;

        touchObjName.text = "";
        touchObjInfo.text = "";

        for(int i = 0; i < maxHp; ++i)
        {
            lifeBowls[i].SetActive(true);
        }

        for(int i = 0; i < hp; ++i)
        {
            lifes[i].SetActive(true);
        }

        StartCoroutine(EnableCollider());
    }

    private void FixedUpdate()
    {
        if (inputL!= Vector3.zero)
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inputL == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            SettingMoveAmount();
            dir = (transform.eulerAngles + inputL) - transform.eulerAngles;
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
        isTouthObj = true;
        GameObject hitObj = collision.gameObject;

        if(hitObj.CompareTag("CameraPoint"))
        {
            pc.SetTarget(hitObj);

            Room room = hitObj.GetComponent<Room>();

            if(!room)
                return;
       
            room.RemoveSprite();
            GameManager.I.SetRoomId(room.GetId());
            
        }

        if (hitObj.CompareTag("Skill"))
        {
            Skill skill = hitObj.GetComponent<Skill>();

            if (!skill) return;

            touchObjName.text = skill.GetName();
            touchObjInfo.text = skill.GetInfo();
        }

        if(hitObj.CompareTag("EnemyBullet"))
        {
            hp--;
            lifes[hp].SetActive(false);

            if(hp == 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;
       
        if (hitObj.CompareTag("Skill") && isGet)
        {
            Skill skill = hitObj.GetComponent<Skill>();

            if (!skill) return;

            isGet = false;

            if (SkillSlot.GetHaveSkill()) return;

            skill.SetId(-1);
            EquipmentSkill(skill);
        }
        else if(hitObj.CompareTag("Goal") && isGet)
        {
            Debug.Log("Goal");
            isGet = false;
            StartCoroutine(GameManager.I.ChangeScene(GameManager.I.GetNextScene()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouthObj = false;

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
        if(context.performed && isTouthObj)
        {
            rb.WakeUp();
            isGet = true;
        }
    }

    public void CheckInputEastButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DropSkill();
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

                Destroy(skill.gameObject);
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

    private void SettingMoveAmount()
    {
        if (inputL.x == 0) return;

        move = inputL * moveSpeed;
    }

    private void Move()
    {
        rb.velocity = new Vector3(move.x, move.y, 0);
    }

    IEnumerator EnableCollider()
    {
        yield return null;

        playerCollider.enabled = true;
    }

    void Die()
    {
        StartCoroutine(GameManager.I.GameOver());
        //StartCoroutine(GameManager.I.ChangeScene("Title"));
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
}
