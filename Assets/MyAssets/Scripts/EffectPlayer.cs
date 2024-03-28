using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] AnimationClip anim;
    [SerializeField] float animSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, anim.length / animSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
