using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SpriteRenderer>().drawMode == SpriteDrawMode.Tiled)
        {
            GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
