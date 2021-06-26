using Char;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;

    [Range(1f, 10f)]
    public float movementSpeed;

    private float _horizontalMove = 0f;
    private bool _jump = false;

    private void Update() {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        
        if (Input.GetAxisRaw("Vertical") > 0) {
            _jump = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);
        _jump = false;
    }
}
