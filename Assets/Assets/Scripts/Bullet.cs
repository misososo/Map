using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    
    Vector3 dir;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    
    Vector3 reflect;
    [SerializeField] int reflectNum;
    
    [SerializeField] int penetrationNum;
    
    [SerializeField] int productScale = 1;

    [SerializeField] int divisionNum;
    [SerializeField] bool isDivision;
    Bullet divisionBullet;
    Vector3 divisionDir;

    [SerializeField] string target;

    int oldRoomId;

    // Start is called before the first frame update
    void Start()
    {
        

        oldRoomId = GameManager.I.GetNowRoomId();
        transform.localScale *= productScale;
        
        if (isDivision)
        {
           
            divisionBullet = Instantiate(this, transform.position, Quaternion.identity);
            divisionDir.x = Random.Range(-1.0f, 1.0f);
            divisionDir.y = Random.Range(-1.0f, 1.0f);
            divisionBullet.SetProductScale(1);
            divisionBullet.SetIsDivision(false);
            divisionBullet.SetDir(divisionDir.normalized);
            divisionBullet = null;
            
        }

        Destroy(gameObject, lifeTime);        
    }

    // Update is called once per frame
    void Update()
    {
        if (oldRoomId != GameManager.I.GetNowRoomId())
        {
            Destroy(gameObject);
        }
    }

    public void SetDir(Vector3 v)
    {
        rb = GetComponent<Rigidbody2D>();
        dir = v;

        rb.velocity = dir * moveSpeed;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
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
        if (collision.gameObject.CompareTag(target))
        {
            if (penetrationNum <= 0)
            {
                Destroy(gameObject);
            }

            penetrationNum--;
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

    public void PlusPenetrationNum()
    {
        penetrationNum++;
    }

    public void MinusPenetrationNum()
    {
        penetrationNum--;
    }

    public void PlusProductScale()
    {
        productScale++;
    }

    public void MinusProductScale()
    {
        productScale--;
    }

    public void PlusDivisionNum()
    {
        divisionNum++;

        if(divisionNum > 0)
        {
            isDivision = true;
        }
    }

    public void MinusDivisionNum()
    {
        divisionNum--;

        if(divisionNum <= 0)
        {
            isDivision = false;
        }
    }

    public void SetReflectNum(int data)
    {
        reflectNum = data;
    }

    public void SetPenetrationNum(int data)
    {
        penetrationNum = data;
    }

    public void SetProductScale(int data)
    {
        productScale = data;
    }

    public void SetDivisionNum(int data)
    {
        divisionNum = data;
    }

    public void SetIsDivision(bool b)
    {
        isDivision = b;
    }

    public int GetReflectNum()
    {
        return reflectNum;
    }

    public int GetPenetrationNum()
    {
        return penetrationNum;
    }

    public int GetProductScale()
    {
        return productScale;
    }

    public int GetDivisionNum()
    {
        return divisionNum;
    }

    public bool GetIsDivision()
    {
        return isDivision;
    }
}
