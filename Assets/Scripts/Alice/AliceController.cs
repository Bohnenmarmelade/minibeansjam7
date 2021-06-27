using UnityEngine;

public class AliceController : MonoBehaviour
{

    private Animator _animator;
    private static readonly int PuffTrigger = Animator.StringToHash("PuffTrigger");

    private AliceFxController _fxController;
    public bool IsTouched { get; set; } = false;

    public void Touched()
    {
        Debug.Log("I was touched!");
        IsTouched = true;
        _animator.SetTrigger(PuffTrigger);
        _fxController.Explode();
    }

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _fxController = gameObject.GetComponent<AliceFxController>();
    }

}
