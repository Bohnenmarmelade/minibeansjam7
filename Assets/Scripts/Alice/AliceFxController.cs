using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceFxController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> screamClips;
    [SerializeField] private AudioClip explodeClip;
    
    [SerializeField] AudioSource source;

    private float nextScream = 0;
    private bool _scream = true;
    
    
    void Start()
    {
        nextScream = Time.time + Random.Range(1f, 5f);
    }

    void Update()
    {
        if (_scream && nextScream < Time.time)
        {
            AudioClip clip = screamClips[Random.Range(0, screamClips.Count)];
            source.PlayOneShot(clip);
            nextScream = Time.time + Random.Range(5f, 10f);
        }
        
    }

    public void Explode()
    {
        source.PlayOneShot(explodeClip);
        _scream = false;
    }
}
