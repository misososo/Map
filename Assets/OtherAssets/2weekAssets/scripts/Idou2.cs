using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idou2 : MonoBehaviour
{
    private float MoveSpeed = 3.0f;

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * MoveSpeed, 0, 0);
    }
}
