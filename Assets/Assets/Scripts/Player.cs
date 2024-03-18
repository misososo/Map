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
        if(collision.gameObject.CompareTag("CameraPoint"))
        {
            pc.SetTarget(collision.gameObject);

            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();

            if(sr)
            {
                sr.sprite = null;
                //collision.gameObject.GetComponent<SpriteRenderer>().sprite = sr.sprite;
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
