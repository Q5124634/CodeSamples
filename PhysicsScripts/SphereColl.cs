using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColl : MonoBehaviour
{
    public float Radius;
    public GameObject LastCollided;
    public GameObject LastCollidedSphere;
    
    void Start()
    {
        Radius = transform.localScale.x * 0.5f; //sets radius
    }
    void FixedUpdate()
    {

        if (Math.Abs(GetComponent<RigidBody>().Velocity.y) > 0.9f) //if the y velocity is greater than 0.9 (gravity) the object is not grounded
        {
            GetComponent<RigidBody>().IsGrounded = false; //set to false
        }

        if (LastCollided != null) //if the last collided has a value
        {

            if (ManageColl.CheckForBoxCollision(LastCollided.GetComponent<BoxColl>().Max, LastCollided.GetComponent<BoxColl>().Min, this.gameObject))
            {
                GetComponent<RigidBody>().IsGrounded = true; //if collision occurs and the last object does not change it sets grounded to true
            }
            else
            {
                GetComponent<RigidBody>().IsGrounded = false; //sets to false if not
            }
        }
    }
}