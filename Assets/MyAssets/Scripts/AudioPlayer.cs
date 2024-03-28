using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, audioSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
