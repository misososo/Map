using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector3 inputL;
    private Vector3 inputR;

    private Vector3 move;
    [SerializeField] float moveSpeed;

    Vector3 dir;

    [SerializeField] PlayerCamera pc;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletShot;
    [SerializeField] Bullet bullet;
    [SerializeField] float shotSpan;
    float firstShotSpan;

    //[SerializeField] SkillDatas skillDatas;
    //[SerializeField] List<Skill> allSkillData;
    //[SerializeField] List<Skill> allSkill;
    [SerializeField] SkillSlot SkillSlot;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstShotSpan = shotSpan;
        
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
              
                Instantiate(bullet, bulletShot.transform.position, Quaternion.identity);
            }
            
        }
        else
        {
            shotSpan = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObj = collision.gameObject;

        if(hitObj.CompareTag("CameraPoint"))
        {
            pc.SetTarget(hitObj);

            SpriteRenderer sr = hitObj.GetComponent<SpriteRenderer>();

            if(!sr)return;
            
            sr.sprite = null;
        }
        else if(hitObj.CompareTag("Skill"))
        {
            Skill skill = hitObj.GetComponent<Skill>();

            if(!skill)return;

            for(int i = 0; i < GameManager.I.GetSkillNum(); ++i)
            {
                if(skill.GetId() == GameManager.I.GetSkill(i).GetId())
                {
                    SkillSlot.SetHaveSkill(GameManager.I.GetSkill(i));
                    GameManager.I.GetSkill(i).GrantSkill(ref bullet);
                }

                
            }        
        }
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

    private void SettingMoveAmount()
    {
        if (inputL.x == 0) return;

        
        //Look((input + transform.position) - transform.position);

        move = inputL * moveSpeed;
    }

    private void Move()
    {
        rb.velocity = new Vector3(move.x, move.y, 0);
    }
}
