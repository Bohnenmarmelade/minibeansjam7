using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomFXController : MonoBehaviour
{
    [SerializeField] private AudioClip fartClip;
    [SerializeField] AudioSource source;



    public void Fart()
    {
        source.clip = fartClip;
        source.PlayOneShot(fartClip);   
    }
}
