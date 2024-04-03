using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;
    float lifeTime;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(int index, float lifeTime)
    {
        audioSource.clip = clips[index];
        audioSource.Play();
        Destroy(gameObject, lifeTime);
    }

    public void PlayAudio(int index)
    {
        lifeTime = clips[index].length;
        audioSource.clip = clips[index];
        audioSource.Play();
        Destroy(gameObject, lifeTime);
    }
}
