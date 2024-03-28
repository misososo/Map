using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : Enemy
{
    GameObject player;
    [SerializeField] float maxWarpSpan;
    [SerializeField] float minWarpSpan;
    float warpSpan;
    [SerializeField] float stealthTime;
    [SerializeField] Collider2D enemyCollider;
    Vector3 warpPos;

    [SerializeField] GameObject fakePrefab;
    GameObject fake;

    [SerializeField] GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        warpSpan = Random.Range(minWarpSpan, maxWarpSpan);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        warpSpan -= Time.deltaTime;

        if(warpSpan <= 0)
        {
            warpSpan = Random.Range(minWarpSpan, maxWarpSpan);
            StartCoroutine(Warp());
        }
    }

    IEnumerator Warp()
    {
        enemyCollider.enabled = false;
        sr.enabled = false;

        warpPos = player.transform.position;

        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        fake = Instantiate(fakePrefab, warpPos, Quaternion.identity);

        yield return new WaitForSeconds(stealthTime);

        Destroy(fake);

        enemyCollider.enabled = true;
        sr.enabled = true;

        transform.position = warpPos;
    }
}
