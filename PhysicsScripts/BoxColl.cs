using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColl : MonoBehaviour
{
    public Vector3 Max;
    public Vector3 Min;

    public float Width;
    public float Height;
    public float Depth;

    public Vector3[] Vertex;
    void Start()
    {
        //sets the boxes width height and depth
        Width = transform.localScale.x;
        Height = transform.localScale.y;
        Depth = transform.localScale.z;


    }
    
    void FixedUpdate()
    {
        //sets the bounds of the box
        Bounds bounds = GetComponent<MeshRenderer>().bounds;
        Max.x = bounds.max.x;
        Max.y = bounds.max.y;
        Max.z = bounds.max.z;
        Min.x = bounds.min.x;
        Min.y = bounds.min.y;
        Min.z = bounds.min.z;
    }


}
