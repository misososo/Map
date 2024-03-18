using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector3 input;

    private Vector3 move;
    [SerializeField] float moveSpeed;

    Vector3 dir;

    [SerializeField] PlayerCamera pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (input!= Vector3.zero)
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(input == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            SettingMoveAmount();
            dir = (transform.eulerAngles + input) - transform.eulerAngles;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CameraPoint"))
        {
            pc.SetTarget(collision.gameObject);
        }
    }

    public void CheckInputLeftStick(InputAction.CallbackContext context)
    {
        input.x = context.ReadValue<Vector2>().x;
        input.y = context.ReadValue<Vector2>().y;
    }

    private void SettingMoveAmount()
    {
        if (input.x == 0) return;

        
        //Look((input + transform.position) - transform.position);

        move = input * moveSpeed;
    }

    private void Move()
    {
        rb.velocity = new Vector3(move.x, move.y, 0);
    }
}
