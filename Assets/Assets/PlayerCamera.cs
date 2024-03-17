using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float height;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = target.transform.position;
        pos.y = pos.y + height;
        pos.z = -10;
        transform.position = pos;
    }
}
