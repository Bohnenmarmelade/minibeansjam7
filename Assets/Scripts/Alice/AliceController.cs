using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceController : MonoBehaviour
{

    private Animator _animator;
    private static readonly int PuffTrigger = Animator.StringToHash("PuffTrigger");
    public bool IsTouched { get; set; } = false;

    public void Touched()
    {
        Debug.Log("I was touched!");
        IsTouched = true;
        _animator.SetTrigger(PuffTrigger);
    }

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

}
