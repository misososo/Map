using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mawaru2 : MonoBehaviour
{
    [SerializeField] private Vector3 _center = Vector3.zero;

    [SerializeField] private Vector3 _axis = Vector3.up;

    [SerializeField] private float _period = 2;

    [SerializeField] private bool _updataRotaion = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        var angleAxis = Quaternion.AngleAxis(360 / _period * Time.deltaTime, _axis);
        var pos = tr.position;

        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;

        if(_updataRotaion)
        {
            tr.rotation = tr.rotation * angleAxis;
        }
    }
}
