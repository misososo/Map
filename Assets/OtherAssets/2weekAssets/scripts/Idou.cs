using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idou : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] int dir;
    [SerializeField] float moveSpeedX;
    [SerializeField] float moveSpeedY;
    Vector3 startPos;
    Vector3 pos;
    [SerializeField] float firstAngleX;
    [SerializeField] float firstAngleY;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x = startPos.x + Mathf.Cos(dir * moveSpeedX * time + firstAngleX) * radius;
        pos.y = startPos.y + Mathf.Sin(dir * moveSpeedY * time + firstAngleY) * radius;
        transform.position = pos;

        time += Time.deltaTime;
    }

    private void OnEnable()
    {
        time = 0;
    }
}  

