using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Block _block;
    float w, a, s, d, z;
    
    // Start is called before the first frame update
    void Start()
    {
        _block = GetComponent<Block>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = GetUpInput() + GetDownInput() + GetLeftInput() + GetRightInput();
        if (direction != Vector3.zero)
        {
            if (_block.TryMove(direction)) 
                GameManager.SaveGameState();
        }

        if (GetInputZ())
        {
            GameManager.UndoMove();
        }
        
    }

    bool GetInputZ()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            z = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return true;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            z += Time.deltaTime;
            if (z > GameManager.inputInterval)
            {
                z = 0f;
                return true;
            }
        }
        return false;
    }
    
    Vector3 GetUpInput()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            w = 0f;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector3.up;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            w += Time.deltaTime;
            if (w > GameManager.inputInterval)
            {
                w = 0f;
                return Vector3.up;
            }
        }
        return Vector3.zero;
    }
    Vector3 GetDownInput()
    {
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            s = 0f;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector3.down;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            s += Time.deltaTime;
            if (s > GameManager.inputInterval)
            {
                s = 0f;
                return Vector3.down;
            }
        }
        return Vector3.zero;
    }
    Vector3 GetLeftInput()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            a = 0f;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector3.left;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            a += Time.deltaTime;
            if (a > GameManager.inputInterval)
            {
                a = 0f;
                return Vector3.left;
            }
        }
        return Vector3.zero;
    }
    Vector3 GetRightInput()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            d = 0f;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector3.right;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            d += Time.deltaTime;
            if (d > GameManager.inputInterval)
            {
                d = 0f;
                return Vector3.right;
            }
        }
        return Vector3.zero;
    }
}
