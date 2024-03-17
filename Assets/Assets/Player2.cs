using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    [SerializeField] Animator anim;
    Rigidbody2D rb;

    private Vector3 input;
    private Vector3 keep;

    private Vector3 move;
    [SerializeField] float moveSpeed;

    [SerializeField] float rotationSpeed;

    [SerializeField] float jumpPower;
    int jumpNum = 2;

    bool isJump = false;

    [SerializeField] float avoidLength;

    [SerializeField] GroundCheck gc;
    
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (input.x != 0)
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveSpeed", Mathf.Abs(move.x));

        if (rb.velocity.y < -0.1f)
        {
            anim.SetBool("JumpDown", true);
        }
        else
        {
            anim.SetBool("JumpDown", false);
        }

        SettingMoveAmount();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AirJumpUp")) return;
        
        if (gc.IsGround)
        {
            if (input == Vector3.zero)
            {
                anim.Play("Idle");
                rb.velocity = Vector3.zero;
            }
            else
            {
                //Debug.Log(count + ", " + gc.IsGround);
                anim.Play("Move");
            }
        }

    }

    public void CheckInputLeftStick(InputAction.CallbackContext context)
    {
        input.x = context.ReadValue<Vector2>().x;
    }

    private void SettingMoveAmount()
    {
        if (input.x == 0) return;

        Look((input + transform.position) - transform.position);

        move = input.magnitude * transform.right * moveSpeed;
    }

    private void Move()
    {
        rb.velocity = new Vector3(move.x, rb.velocity.y, 0);
    }

    private void Look(Vector3 dir)
    {
        dir.y = 0;
        dir.z = 0;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.FromToRotation(transform.forward, Vector3.right);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (gc.IsGround)
        {
            jumpNum = 2;
        }

        if (context.performed && jumpNum > 0)
        {
            gc.SetIsGroung(false);
            //Debug.Log(count + ", " + gc.IsGround);
            anim.Play("AirJumpUp", 0, 0);


            //gc.gameObject.SetActive(false);
            //wcL.SetIsWall(false);
            //wcR.SetIsWall(false);
            jumpNum--;
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0, jumpPower));
            //anim.SetBool("JumpDown", false);
            //anim.SetBool("JumpUp", true);
        }
    }

    public void Avoid(InputAction.CallbackContext context)
    {
        if (context.performed && gc.IsGround && input.x != 0)
        {
            StartCoroutine(AvoidCoroutine());
        }
    }

    IEnumerator AvoidCoroutine()
    {
        keep = input;

        yield return null;
    }

    public Vector3 GetInput()
    {
        return input;
    }
}
