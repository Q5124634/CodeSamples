using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{


    public float Mass = 1;
    public float Drag;
    public float AngularDrag;
    public float Bounce = 1.8f;
    private float SetKineticFriction;
    public float SetKineticCoefficient = 0.0015f; //set to a natural number

    public bool IsGrounded;
    public bool IsMoving;

    public Vector3 Force = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.0f);

    public Vector3 Acceleration
    {
        get { return _acceleration; }
        set { _acceleration = value; if (value != Vector3.zero) { LastAcceleration = value; } }
    }

    public Vector3 Gravity = new Vector3(0.0f, -9.81f, 0.0f);

    private Vector3 _acceleration;

    private Vector3 Friction;
    public bool UseGravity = true;
    public bool UseFriction = true;

    public Vector3 LastAcceleration;

    void Start()
    {
        SetKineticFriction = SetKineticCoefficient * (Mass * -Gravity.y); //sets the value for kinetic friction
    }
    void FixedUpdate()
    {
        if (UseGravity) //if use gravity is true
        {
            Vector3 Pos = transform.position; //move the object
            Acceleration = (Force + Gravity); //with increased acceleration
            Vector3 NewVelocity = SetEuler(Velocity, Time.deltaTime, Acceleration); //and a new changing velocity
            Velocity = NewVelocity;
            Vector3 NewPos = SetEuler(Pos, Time.deltaTime, Velocity); //over time

            if (this.CompareTag("Sphere")) //if its a sphere it has different properties
            {
                if (NewPos.y < this.GetComponent<SphereColl>().Radius) //if its new position is greater than its radius on the y
                {
                    NewPos.y = this.GetComponent<SphereColl>().Radius; //set its position to be its radius
                    IsGrounded = true; //set it to be grounded
                }
            }

            if (this.CompareTag("Box")) //check if the object is a box
            {
                if (NewPos.y < this.transform.position.y - this.GetComponent<BoxColl>().Min.y) //set its y value to half length
                {
                    NewPos.y = this.transform.position.y - this.GetComponent<BoxColl>().Min.y; //set the new location to be 1/2 of the ;engh above its grounded position
                    IsGrounded = true; //set grounded to true
                }
            } //check the reference point of the object

            this.transform.position = new Vector3(NewPos.x, NewPos.y, NewPos.z); //transform the objects position
            Force = new Vector3(0, 0, 0); //Reset force
            Acceleration = new Vector3(0, 0, 0); //reset acceleration
        }

        if (UseFriction) //if using friction
            {
                if (IsGrounded) //and is grounded
                {
                    Friction = Velocity; ///set to velocity
                    Force = new Vector3(-Friction.x, 0, -Friction.z); //set force
                    Force = Force * SetKineticFriction; //by kinetic friction so it slows down 
                    Velocity = new Vector3(ZeroOutVelocity(Velocity.x + Force.x), Velocity.y, ZeroOutVelocity(Velocity.z + Force.z)); //velocity slowly comes to a stop
                    Force = new Vector3(0, 0, 0); //once stopped no force is acting on the object
                }
            }

    } //use setting friction between 2 objects
    Vector3 SetEuler(Vector3 Pos, float time, Vector3 Velocity) //setting up my own euler
    {
        return Pos + (time * Velocity); //eulerEquasion
    }
    float ZeroOutVelocity(float Vel)
    {
        if (Vel < 0.0001f && Vel > -0.0001f) // if velocity is so low
        {
            return 0; //set it to 0
        }
        else
        {
            return Vel; //if it is not, velocity stays the same
        }

    }
    private void OnDrawGizmos() //drawing all gizmos 
    {
        if (CompareTag("Sphere"))
        {
            //if the object is a sphere set no gizmos
        }

        else

        {
            Gizmos.color = Color.blue;//set them to blue
            Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
            Gizmos.color = Color.green; //set them to green
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(1, 1, 1) / 2), new Vector3(0.1f, 0.1f, 0.1f)); //apply to the corners of each selected object
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(1, 1, -1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(1, -1, 1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(1, -1, -1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(-1, 1, 1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(-1, 1, -1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(-1, -1, 1) / 2), new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawCube(transform.TransformPoint(new Vector3(-1, -1, -1) / 2), new Vector3(0.1f, 0.1f, 0.1f));



        }
        Gizmos.color = Color.grey;
        Gizmos.DrawCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f)); //sets center gizmo to grey
    }
}