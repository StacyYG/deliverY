﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{ 
    public bool moveable = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction;
    }

    public bool TryMove(Vector3 direction)
    {
        if (!moveable) return false;
        if (GetColliderAt(transform.position + direction) is null)
        {
            Move(direction);
            return true;
        }

        if (GetColliderAt(transform.position + direction).GetComponent<Block>().TryMove(direction))
        {
            Move(direction);
            return true;
        }
        
        return false;
    }
    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector2.zero);

        return hit.collider;
    }
    
}



