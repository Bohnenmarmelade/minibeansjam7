using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    private float _leftBoundary;
    private float _rightBoundary;
    private float _posY;
    
    private void Start()
    {
        //find left and right boundary based on gameObjects box collider
        BoxCollider2D boxCollider2D = gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>();
        var bounds = boxCollider2D.bounds; //using bounds is ok because box collider is always parallel to world axes
        _leftBoundary = bounds.center.x - bounds.extents.x;
        _rightBoundary = bounds.center.x + bounds.extents.x;
        _posY = bounds.center.y + bounds.extents.y;
    }

    public GameObject Spawn(GameObject spawningObject)
    {
        float extendX = 0;
        float extendY = 0;

        BoxCollider2D boxCollider2D = spawningObject.GetComponent<BoxCollider2D>();
        CircleCollider2D circleCollider2D = spawningObject.GetComponent<CircleCollider2D>();
        CapsuleCollider2D capsuleCollider2D = spawningObject.GetComponent<CapsuleCollider2D>();

        if (boxCollider2D != null)
        {
            extendX = boxCollider2D.size.x / 2;
            extendY = boxCollider2D.size.y / 2;
        } else if (circleCollider2D)
        {
            extendX = circleCollider2D.radius;
            extendY = circleCollider2D.radius;
        } else if (capsuleCollider2D)
        {
            extendX = capsuleCollider2D.size.x / 2;
            extendY = capsuleCollider2D.size.y / 2;
        }
        
        spawningObject.GetComponent<CircleCollider2D>();
        
        float x = Random.Range(_leftBoundary + extendX, _rightBoundary - extendX);

        Vector3 pos = new Vector2(x, _posY + extendY);
        
        return Instantiate(spawningObject, pos, Quaternion.identity);
    }
}
