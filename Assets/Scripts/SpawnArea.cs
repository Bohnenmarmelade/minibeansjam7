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
        Debug.Log(spawningObject.GetComponent<Collider2D>().bounds);
        
        
        float x = Random.Range(_leftBoundary, _rightBoundary);

        Vector3 pos = new Vector2(x, 0);
        
        GameObject g = Instantiate(spawningObject, pos, Quaternion.identity);
        
        float y = _posY + g.GetComponent<Collider2D>().bounds.extents.y;
        pos = new Vector2(x, y);
        g.transform.position = pos;

        return g;
    }
}
