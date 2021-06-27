using System;
using System.Collections;
using System.Collections.Generic;
using Char;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    
    private Vector3 upLeft = new Vector3(-0.7f, 0.7f, 0);
    private Vector3 upRight = new Vector3(0.7f, 0.7f, 0);
    private Vector3 downLeft = new Vector3(-0.7f, -0.7f, 0);
    private Vector3 downRight = new Vector3(0.7f, -0.7f, 0);

    private SpriteRenderer _spriteRenderer;

    private AliceController _alice;
    private PlayerController _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _player = GameObject.FindObjectOfType<PlayerController>();
    }

    public void NextAlice()
    {
        //find next alice
        _player = GameObject.FindObjectOfType<PlayerController>();
        _alice = GameObject.FindObjectOfType<AliceController>();
        
    }
    private void FixedUpdate()
    {
        Vector3 dir = (_alice.transform.position - _player.transform.position).normalized;
        Vector3 newDir = SnapTo(dir, 45f);
        SetCorrectSprite(newDir);
    }

    private void SetCorrectSprite(Vector3 dir)
    {
        int i = 0;
        if (dir == Vector3.left)
        {
            i = 0;
        } else if (Math.Abs(dir.x - downLeft.x) < .1 && Math.Abs(dir.y - downLeft.y) < .1)
        {
            i = 1;
        }else if (dir == Vector3.down)
        {
            i = 2;
        }else if (Math.Abs(dir.x - downRight.x) < .1 && Math.Abs(dir.y - downRight.y) < .1)
        {
            i = 3;
        }else if (dir == Vector3.right)
        {
            i = 4;
        }else if (Math.Abs(dir.x - upRight.x) < .1 && Math.Abs(dir.y - upRight.y) < .1)
        {
            i = 5;
        }else if (dir == Vector3.up)
        {
            i = 6;
        }else if (Math.Abs(dir.x - upLeft.x) < .1 && Math.Abs(dir.y - upLeft.y) < .1)
        {
            i = 7;
        }
        
        Sprite sprite = sprites[i];
        _spriteRenderer.sprite = sprite;
    }
    Vector3 SnapTo(Vector3 v3, float snapAngle) {
        float   angle = Vector3.Angle (v3, Vector3.up);
        if (angle < snapAngle / 2.0f)          // Cannot do cross product 
            return Vector3.up * v3.magnitude;  //   with angles 0 & 180
        if (angle > 180.0f - snapAngle / 2.0f)
            return Vector3.down * v3.magnitude;
     
        float t = Mathf.Round(angle / snapAngle);
        float deltaAngle = (t * snapAngle) - angle;
     
        Vector3 axis = Vector3.Cross(Vector3.up, v3);
        Quaternion q = Quaternion.AngleAxis (deltaAngle, axis);
        return q * v3;
    }
}
