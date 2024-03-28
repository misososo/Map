using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Vector3 rotationAmount;
    [SerializeField] float turnTime;
    bool isTurn = false;

    [SerializeField] GameObject hitEffectPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 2);

        if(random == 0)
        {
            rotationAmount *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAmount);

        turnTime -= Time.deltaTime;

        if(turnTime <= 0 && !isTurn)
        {
            isTurn = true;
            rotationAmount *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        }
    }
}
