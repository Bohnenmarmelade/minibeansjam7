using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFXController : MonoBehaviour
{
    
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip attackClip;
    
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource attackSource;

    private bool _isTapping = false;



    public void Attack()
    {
        _isTapping = false;
        source.Stop();
        attackSource.PlayOneShot(attackClip);   
    }

    public void StartWalk()
    {
        if (!_isTapping)
        {
            Debug.Log("Playing: Tap");
            source.clip = walkClip;
            source.Play();
            source.loop = true;
            _isTapping = true;
        }
    }

    public void StopWalk()
    {
        Debug.Log("Stop: Tap");
        source.Stop();
        _isTapping = false;

    }
}
