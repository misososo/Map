using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] int atk;
    [SerializeField] Collider2D exCollider;
    [SerializeField] GameObject effect;

    SpriteRenderer sr;

    [SerializeField] float flushSpan = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explosion()
    {
        GameManager.I.PlaySE((int)GameManager.SE.fire, lifeTime, transform.position);

        yield return Flush(lifeTime, flushSpan);

        exCollider.enabled = true;

        yield return null;

        Destroy(gameObject);
    }

    IEnumerator Flush(float time, float span)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(span);
            time -= span;

            sr.enabled = false;
            
            yield return new WaitForSeconds(span);
            time -= span;

            sr.enabled = true;
        }
    }

    public int GetAtk()
    {
        return atk;
    }

    private void OnDestroy()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        GameManager.I.PlaySE((int)GameManager.SE.bom, transform.position);
    }
}
