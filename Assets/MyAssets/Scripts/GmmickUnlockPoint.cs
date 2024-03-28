using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmmickUnlockPoint : MonoBehaviour
{
    bool isUnlock = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isUnlock = true;
        }
    }

    public bool GetIsUnlock()
    {
        return isUnlock;
    }
}
