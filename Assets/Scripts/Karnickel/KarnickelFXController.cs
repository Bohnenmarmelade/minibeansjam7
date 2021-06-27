using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarnickelFXController : MonoBehaviour
{

    [SerializeField] private List<AudioClip> tooLateClips;
    [SerializeField] private List<AudioClip> owClips;
    [SerializeField] private AudioClip tapClip;
    
    [SerializeField] AudioSource tapSource;
    [SerializeField] AudioSource owSource;
    [SerializeField] AudioSource tooLateSource;

    private float nextTooLate = 0;
    private bool _isTapping = false;
    
    
    void Start()
    {
        nextTooLate = Time.time + Random.Range(1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTooLate < Time.time)
        {
            AudioClip clip = tooLateClips[Random.Range(0, tooLateClips.Count)];
            tooLateSource.PlayOneShot(clip);
            nextTooLate = Time.time + Random.Range(5f, 20f);
        }
        
    }

    public void Ow()
    {
        AudioClip clip = owClips[Random.Range(0, owClips.Count)];
        owSource.PlayOneShot(clip);
        tooLateSource.Stop();
    }

    public void StartTap()
    {
        if (!_isTapping)
        {
            tapSource.clip = tapClip;
            tapSource.Play();
            tapSource.loop = true;
            _isTapping = true;
        }
    }

    public void StopTap()
    {
        tapSource.Stop();
        _isTapping = false;

    }
}
