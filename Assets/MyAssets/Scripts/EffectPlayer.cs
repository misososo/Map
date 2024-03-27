using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] AnimationClip anim;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, anim.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
