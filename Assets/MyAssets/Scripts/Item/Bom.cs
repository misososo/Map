using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] int atk;
    [SerializeField] Collider2D exCollider;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(lifeTime);

        exCollider.enabled = true;

        yield return null;

        Destroy(gameObject);
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
