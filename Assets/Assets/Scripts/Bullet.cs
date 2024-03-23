using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject gun;
    Vector3 dir;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] PlayerCamera pc;
    Vector3 pcOldPos;

    Vector3 reflect;
    [SerializeField] int reflectNum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = gun.transform.up;
        pcOldPos = pc.transform.position;

        Destroy(gameObject, lifeTime);

        rb.velocity = dir * moveSpeed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (pcOldPos != pc.transform.position)
        {
            pcOldPos = pc.transform.position;

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(reflectNum);

        if (collision.gameObject.CompareTag("Wall"))
        {
            if (reflectNum <= 0)
            {
                Destroy(gameObject);
            }

            //Debug.Log(rb.velocity);
            reflectNum--;
            //Vector3 hitPos = collision.ClosestPoint(transform.position);
            reflect = Vector3.Reflect(transform.up, collision.contacts[0].normal).normalized;

            transform.rotation = Quaternion.LookRotation(Vector3.forward, reflect);
            rb.velocity = transform.up * moveSpeed;
            


            //Debug.Log(reflect);
        }
         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("AAAA");
            Destroy(gameObject);
        }
    }

    public void PlusReflectNum()
    {
        reflectNum++;
    }

    public void MinusReflectNum()
    {
        reflectNum--;
    }

    public int GetReflect()
    {
        return reflectNum;
    }
}
