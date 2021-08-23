using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInfo : MonoBehaviour
{
    [HideInInspector]
    public Vector2 leftBottom,rightBottom,leftUp,rightUp;

    public Vector2 center;
    public Vector2 size;
    public Vector2 touch;
    public bool touched;
    public bool playerCollides;
    
    public bool reachable=false;
    
    private BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        center = col.bounds.center;
        size = col.size;
        rightUp = (center+new Vector2(size.x/2+col.edgeRadius,size.y/2+col.edgeRadius));
        rightBottom = (center+new Vector2(size.x/2+col.edgeRadius,-size.y/2-col.edgeRadius));
        leftUp = (center+new Vector2(-size.x/2-col.edgeRadius,size.y/2+col.edgeRadius));
        leftBottom = (center+new Vector2(-size.x/2-col.edgeRadius,-size.y/2-col.edgeRadius));
        Debug.DrawLine(center,rightUp,Color.blue,10,false);
        Debug.DrawLine(center,rightBottom,Color.blue,10,false);
        Debug.DrawLine(center,leftUp,Color.blue,10,false);
        Debug.DrawLine(center,leftBottom,Color.blue,10,false);
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            playerCollides = true;
            touch = other.contacts[0].point;
            Debug.DrawLine(touch,center,Color.magenta,10,false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            playerCollides = false;
        }
    }
}
