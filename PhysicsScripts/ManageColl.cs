using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageColl : MonoBehaviour
{   private GameObject[] Boxes;
    private GameObject[] Spheres;

    Vector3 Box1Max;
    Vector3 Box1Min;
    Vector3 Box2Max;
    Vector3 Box2Min;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Spheres = GameObject.FindGameObjectsWithTag("Sphere"); //fills the array with spheres
        Boxes = GameObject.FindGameObjectsWithTag("Box"); //fills the array with boxes
        SphereOnSphereCollision();
        BoxOnBoxCollisionAABB();
        SphereOnBoxCollisionAABB();
    }
    private void SphereOnBoxCollisionAABB()
    {
        //goes through all boxes and spheres and checks the bounds to see if they collide
        if (Boxes.Length > 0 && Spheres.Length > 0)
        {
            foreach (GameObject Box in Boxes)
            {
                foreach (GameObject Sphere in Spheres)
                {

                    //checks max and min values
                    if (CheckForBoxCollision(Box.GetComponent<BoxColl>().Max, Box.GetComponent<BoxColl>().Min, Sphere))
                    {
                        if ((Sphere.transform.position.x > Box.transform.position.x || Sphere.transform.position.x < Box.transform.position.x) && Sphere.transform.position.y > Box.GetComponent<BoxColl>().Min.y
                            && Sphere.transform.position.y < Box.GetComponent<BoxColl>().Max.y && Sphere.transform.position.z > Box.GetComponent<BoxColl>().Min.z
                            && Sphere.transform.position.z < Box.GetComponent<BoxColl>().Max.z)
                        {
                            //checks to correct sphere on box on the x axis as i split the faces of boxes
                            Vector3 Normalized = Vector3.Normalize(Sphere.transform.position - new Vector3(Box.transform.position.x, Sphere.transform.position.y, Sphere.transform.position.z));
                            float AxisXyz = Vector3.Magnitude(new Vector3(Box.transform.position.x, Sphere.transform.position.y, Sphere.transform.position.z) - Sphere.transform.position);
                            //checking distances to see if it needs to change the transform of the object due to its penetration distance
                            if (AxisXyz < Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Width / 2)
                            {
                                float PenetrationDistance = AxisXyz - (Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Width / 2);

                                if (!Sphere.GetComponent<RigidBody>().IsGrounded)
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        Box.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
                                    }
                                    else
                                    {
                                        if (Math.Abs(Vector3.Magnitude(Sphere.GetComponent<RigidBody>().Velocity)) > 0.5f)
                                        {
                                            Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        }

                                    }
                                }
                                else
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        if (Math.Abs(Vector3.Magnitude(Box.GetComponent<RigidBody>().Velocity)) > 0.5f)
                                        {
                                            Box.transform.position += (((PenetrationDistance)) * Normalized);
                                        }
                                    }
                                }


                            }; //sets corrections
                            if (Box.GetComponent<RigidBody>().UseGravity)
                            {
                                Vector3 NewVelocity1;
                                Vector3 NewVelocity2;
                                NewVelocity1 = Box.GetComponent<RigidBody>().Velocity;
                                NewVelocity1 += Vector3.Project(Sphere.GetComponent<RigidBody>().Velocity, Sphere.transform.position - Box.transform.position);
                                NewVelocity1 -= Vector3.Project(Box.GetComponent<RigidBody>().Velocity, Box.transform.position - Sphere.transform.position);
                                NewVelocity2 = Sphere.GetComponent<RigidBody>().Velocity;
                                NewVelocity2 += Vector3.Project(Box.GetComponent<RigidBody>().Velocity, Sphere.transform.position - Box.transform.position);
                                NewVelocity2 -= Vector3.Project(Sphere.GetComponent<RigidBody>().Velocity, Box.transform.position - Sphere.transform.position);

                                Sphere.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity2.x, Sphere.GetComponent<RigidBody>().Velocity.y, NewVelocity2.z);
                                Box.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity1.x, Box.GetComponent<RigidBody>().Velocity.y, NewVelocity1.z);
                            }
                            else
                            {
                                Vector3 Velocity;
                                Vector3 reflectedVelocity;
                                Velocity = Sphere.GetComponent<RigidBody>().Velocity;


                                reflectedVelocity.x = Sphere.GetComponent<RigidBody>().Velocity.x - (Sphere.GetComponent<RigidBody>().Bounce * (Velocity.x));
                                Sphere.GetComponent<RigidBody>().Velocity.x = reflectedVelocity.x;
                            } //simulates the reaction on the x axis
                        }

                        else if ((Sphere.transform.position.y > Box.transform.position.y || Sphere.transform.position.y < Box.transform.position.y) && Sphere.transform.position.x > Box.GetComponent<BoxColl>().Min.x
                            && Sphere.transform.position.x < Box.GetComponent<BoxColl>().Max.x && Sphere.transform.position.z > Box.GetComponent<BoxColl>().Min.z
                            && Sphere.transform.position.z < Box.GetComponent<BoxColl>().Max.z)
                        {

                            //checks to correct sphere on box on the y axis as i split the faces of boxes
                            Vector3 Normalized = Vector3.Normalize(Sphere.transform.position - new Vector3(Sphere.transform.position.x, Box.transform.position.y, Sphere.transform.position.z));
                            float AxisXyz = Vector3.Magnitude(new Vector3(Sphere.transform.position.x, Box.transform.position.y, Sphere.transform.position.z) - Sphere.transform.position);
                            //checking distances to see if it needs to change the transform of the object due to its penetration distance
                            if (AxisXyz < Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Height / 2)
                            {
                                float PenetrationDistance = AxisXyz - (Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Height / 2);



                                if (!Sphere.GetComponent<RigidBody>().IsGrounded)
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        Box.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
                                    }
                                    else
                                    {
                                        if (Math.Abs(Vector3.Magnitude(Sphere.GetComponent<RigidBody>().Velocity)) > 0.5f)
                                        {
                                            Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        }

                                    }
                                }
                                else
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        if (Math.Abs(Vector3.Magnitude(Box.GetComponent<RigidBody>().Velocity)) > 0.5f)
                                        {
                                            Box.transform.position += (((PenetrationDistance)) * Normalized);
                                        }
                                    }
                                }


                            } //sets corrections

                            Vector3 Velocity;
                            Vector3 reflectedVelocity;
                            //checks last collided
                            Sphere.GetComponent<SphereColl>().LastCollided = Box;

                            //sets velocity
                            Velocity = Sphere.GetComponent<RigidBody>().Velocity;

                            //sets reflected velocity
                            reflectedVelocity.y = Sphere.GetComponent<RigidBody>().Velocity.y - (Sphere.GetComponent<RigidBody>().Bounce * (Velocity.y));
                            Sphere.GetComponent<RigidBody>().Velocity.y = reflectedVelocity.y;

                            //if the sphere is higher than the box
                            if (Sphere.transform.position.y > Box.transform.position.y)
                            {
                                //apply force
                                Sphere.GetComponent<RigidBody>().Force -= Sphere.GetComponent<RigidBody>().Gravity;
                                Sphere.GetComponent<RigidBody>().IsGrounded = true;

                            } //simulates on the y axis
                        }

                        else if ((Sphere.transform.position.z > Box.transform.position.z || Sphere.transform.position.z < Box.transform.position.z) && Sphere.transform.position.x > Box.GetComponent<BoxColl>().Min.x
                            && Sphere.transform.position.x < Box.GetComponent<BoxColl>().Max.x && Sphere.transform.position.y > Box.GetComponent<BoxColl>().Min.y
                            && Sphere.transform.position.y < Box.GetComponent<BoxColl>().Max.y)
                        {

                            //checks to correct sphere on box on the z axis as i split the faces of boxes
                            Vector3 Normalized = Vector3.Normalize(Sphere.transform.position - new Vector3(Sphere.transform.position.x, Sphere.transform.position.y, Box.transform.position.z));
                            float AxisXyz = Vector3.Magnitude(new Vector3(Sphere.transform.position.x, Sphere.transform.position.y, Box.transform.position.z) - Sphere.transform.position);
                            //checking distances to see if it needs to change the transform of the object due to its penetration distance
                            if (AxisXyz < Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Depth / 2)
                            {
                                float PenetrationDistance = AxisXyz - (Sphere.GetComponent<SphereColl>().Radius + Box.GetComponent<BoxColl>().Depth / 2);

                                if (!Sphere.GetComponent<RigidBody>().IsGrounded)
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        Box.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
                                    }
                                    else
                                    {
                                        if (Math.Abs(Sphere.GetComponent<RigidBody>().Velocity.z) > 0.5f)
                                        {
                                            Sphere.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                                        }

                                    }
                                }
                                else
                                {
                                    if (!Box.GetComponent<RigidBody>().IsGrounded)
                                    {
                                        if (Math.Abs(Vector3.Magnitude(Box.GetComponent<RigidBody>().Velocity)) > 0.5f)
                                        {
                                            Box.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
                                        }
                                    }
                                }


                            }


                            //sets corrections
                            if (Box.GetComponent<RigidBody>().UseGravity)
                            {
                                Vector3 NewVelocity1;
                                Vector3 NewVelocity2;
                                NewVelocity1 = Box.GetComponent<RigidBody>().Velocity;
                                NewVelocity1 += Vector3.Project(Sphere.GetComponent<RigidBody>().Velocity, Sphere.transform.position - Box.transform.position);
                                NewVelocity1 -= Vector3.Project(Box.GetComponent<RigidBody>().Velocity, Box.transform.position - Sphere.transform.position);
                                NewVelocity2 = Sphere.GetComponent<RigidBody>().Velocity;
                                NewVelocity2 += Vector3.Project(Box.GetComponent<RigidBody>().Velocity,Sphere.transform.position - Box.transform.position);
                                NewVelocity2 -= Vector3.Project(Sphere.GetComponent<RigidBody>().Velocity, Box.transform.position - Sphere.transform.position);

                                Sphere.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity2.x, Sphere.GetComponent<RigidBody>().Velocity.y, NewVelocity2.z);
                                Box.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity1.x, Box.GetComponent<RigidBody>().Velocity.y, NewVelocity1.z);
                            }
                            else
                            {
                                Vector3 Velocity;
                                Vector3 reflectedVelocity;
                                Velocity = Sphere.GetComponent<RigidBody>().Velocity;

                                reflectedVelocity.z = Sphere.GetComponent<RigidBody>().Velocity.z - (Sphere.GetComponent<RigidBody>().Bounce * (Velocity.z));
                                Sphere.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                            }
                        }
                    }

                }

            }
        }
    }
    private void BoxOnBoxCollisionAABB()
    { //goes through all boxes
        if (Boxes.Length > 0)
        {
            for (int Box1 = 0; Box1 < Boxes.Length - 1; Box1++)
            {
                for (int Box2 = Box1 + 1; Box2 < Boxes.Length; Box2++)
                {
                    GetBoxPositions(Boxes[Box1], Boxes[Box2]);

                    if (CheckForBoxCollisionOnBox(Boxes[Box1], Boxes[Box2]))
                    {
                        SetIsGrounded(Boxes[Box1], true); //sets both boxes to grounded
                        SetIsGrounded(Boxes[Box2], true);
                        GetBoxPositions(Boxes[Box1], Boxes[Box2]); //gets positions
                        CollisionWithBoxResponse(Boxes[Box1], Boxes[Box2]); //collides
                    }
                    else
                    {
                        //does nothing otherwise
                    }
                }
            }
        }
    }
    private void SphereOnSphereCollision()
    {
        //loops through spheres
        if (Spheres.Length > 0)
        {
            for (int Sphere1 = 0; Sphere1 < Spheres.Length - 1; Sphere1++)
            {
                for (int Sphere2 = Sphere1 + 1; Sphere2 < Spheres.Length; Sphere2++)
                {
                    if (CalculateCollideSphere(Spheres[Sphere1], Spheres[Sphere2])) //calculates collision
                    {
                        SphereOnSphereCorrection(Spheres[Sphere1], Spheres[Sphere2]); //corrects the collision if needed
                        SimulateCollision(Spheres[Sphere1], Spheres[Sphere2]); //simulates collision
                    }
                }
            }
        }
    }
    bool CalculateCollideSphere(GameObject Sphere1, GameObject Sphere2)
    {
        //calculates the difference between 2 spheres
        double DeltaX = Sphere2.transform.position.x - Sphere1.transform.position.x;
        double DeltaY = Sphere2.transform.position.y - Sphere1.transform.position.y;
        double DeltaZ = Sphere2.transform.position.z - Sphere1.transform.position.z;
         //squares the value for calculation
        double DXSquared = DeltaX * DeltaX;
        double DYSquared = DeltaY * DeltaY;
        double DZSquared = DeltaZ * DeltaZ;

        //works out the sum of the radius
        double SumOfRadius = Sphere1.GetComponent<SphereColl>().Radius + Sphere2.GetComponent<SphereColl>().Radius;
        double RadiusSquared = SumOfRadius * SumOfRadius;

        //works out distance compared to the lengths of the radius for collision
        if (DXSquared + DYSquared + DZSquared <= RadiusSquared)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void SimulateCollision(GameObject Sphere1, GameObject Sphere2)
    {
        //set new velocities
        Vector3 NewVelocity1;
        Vector3 NewVelocity2;

        //calculate new velocities 
        NewVelocity1 = Sphere1.GetComponent<RigidBody>().Velocity;
        NewVelocity1 += Vector3.Project(Sphere2.GetComponent<RigidBody>().Velocity, Sphere2.transform.position - Sphere1.transform.position);
        NewVelocity1 -= Vector3.Project(Sphere1.GetComponent<RigidBody>().Velocity, Sphere1.transform.position - Sphere2.transform.position);

        //for both objects colliding
        NewVelocity2 = Sphere2.GetComponent<RigidBody>().Velocity;
        NewVelocity2 += Vector3.Project(Sphere1.GetComponent<RigidBody>().Velocity, Sphere2.transform.position - Sphere1.transform.position);
        NewVelocity2 -= Vector3.Project(Sphere2.GetComponent<RigidBody>().Velocity, Sphere1.transform.position - Sphere2.transform.position);

        //set the spheres new velocity
        Sphere1.GetComponent<RigidBody>().Velocity = NewVelocity1;
        Sphere2.GetComponent<RigidBody>().Velocity = NewVelocity2;

        //slows collisions to a stop and sets to grounded.
        if (Sphere1.transform.position.y > Sphere2.transform.position.y && Sphere2.GetComponent<RigidBody>().IsGrounded)
        {
            Sphere1.GetComponent<RigidBody>().Force -= Sphere1.GetComponent<RigidBody>().Gravity;
            Sphere1.GetComponent<RigidBody>().IsGrounded = true;
        }
        //slows collisions to a stop and sets to grounded.
        if (Sphere2.transform.position.y > Sphere1.transform.position.y && Sphere1.GetComponent<RigidBody>().IsGrounded)
        {
            Sphere2.GetComponent<RigidBody>().Force -= Sphere2.GetComponent<RigidBody>().Gravity;
            Sphere2.GetComponent<RigidBody>().IsGrounded = true;
        }
    }
    private void SphereOnSphereCorrection(GameObject Sphere1, GameObject Sphere2)
    {
        //corrects collisions by bringing in penetration. 
        Vector3 Normalized = Vector3.Normalize(Sphere1.transform.position - Sphere2.transform.position);
        float AxisXyz = Vector3.Magnitude(Sphere2.transform.position - Sphere1.transform.position);

        //setting penetration distance if its needed
        if (AxisXyz < Sphere1.GetComponent<SphereColl>().Radius + Sphere2.GetComponent<SphereColl>().Radius)
        {
            float PenetrationDistance = AxisXyz - (Sphere1.GetComponent<SphereColl>().Radius + Sphere2.GetComponent<SphereColl>().Radius);

            //transforms the sphere to ensure it does not sink into the ground
            if (!Sphere1.GetComponent<RigidBody>().IsGrounded)
            {
                if (!Sphere2.GetComponent<RigidBody>().IsGrounded)
                {
                    Sphere1.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                    Sphere2.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
                }
                else
                {
                    //if not it transforms the position
                    if (Math.Abs(Vector3.Magnitude(Sphere1.GetComponent<RigidBody>().Velocity)) > 0.5f)
                    {
                        Sphere1.transform.position += (((PenetrationDistance)) * -Normalized);
                    }

                }
            }
            else
            { //checks if the second sphere is grounded
                if (!Sphere2.GetComponent<RigidBody>().IsGrounded)
                {
                    if (Math.Abs(Vector3.Magnitude(Sphere2.GetComponent<RigidBody>().Velocity)) > 0.5f)
                    {
                        Sphere2.transform.position += ((PenetrationDistance) * Normalized);
                    }
                }
            }


        }
    }
    private void GetBoxPositions(GameObject Box1, GameObject Box2)
    {
        //gets box positions min and max
        Box1Max = Box1.GetComponent<BoxColl>().Max;
        Box1Min = Box1.GetComponent<BoxColl>().Min;
        Box2Max = Box2.GetComponent<BoxColl>().Max;
        Box2Min = Box2.GetComponent<BoxColl>().Min;
    }
    private bool CheckForBoxCollisionOnBox(GameObject Box1, GameObject Box2)
    {

        //checks for box on box collision
        if (Box1Min.x > Box2Max.x || Box2Min.x > Box1Max.x || Box1Min.y > Box2Max.y || Box2Min.y > Box1Max.y || Box1Min.z > Box2Max.z || Box2Min.z > Box1Max.z)
        {

        }
        else
        {
            return true;
        }
        return false;

    }
    private void CollisionWithBoxResponse (GameObject Box1, GameObject Box2)
    {
        Vector3 PenetrationDistance;
        Vector3 AbsPenetrationDistance;

        //sets box penetration distance
        float AxisXyz = Vector3.Magnitude(new Vector3(Box1.transform.position.x, Box2.transform.position.y, Box2.transform.position.z) - Box2.transform.position);

        if (AxisXyz < Box1.GetComponent<BoxColl>().Width / 2 + Box2.GetComponent<BoxColl>().Width / 2)
        {
            PenetrationDistance.x = AxisXyz - (Box2.GetComponent<BoxColl>().Width / 2 + Box1.GetComponent<BoxColl>().Width / 2);
        }
        else
            PenetrationDistance.x=300f;


        AxisXyz = Vector3.Magnitude(new Vector3(Box2.transform.position.x, Box1.transform.position.y, Box2.transform.position.z) - Box2.transform.position);

        if (AxisXyz < Box2.GetComponent<BoxColl>().Height / 2 + Box1.GetComponent<BoxColl>().Height / 2)
        {
            PenetrationDistance.y = AxisXyz - (Box2.GetComponent<BoxColl>().Height / 2 + Box1.GetComponent<BoxColl>().Height / 2);
        }
        else
            PenetrationDistance.y = 300f;

        AxisXyz = Vector3.Magnitude(new Vector3(Box2.transform.position.x, Box2.transform.position.y, Box1.transform.position.z) - Box2.transform.position);

        if (AxisXyz < Box2.GetComponent<BoxColl>().Depth / 2 + Box1.GetComponent<BoxColl>().Depth / 2)
        {
            PenetrationDistance.z = AxisXyz - (Box2.GetComponent<BoxColl>().Depth / 2 + Box1.GetComponent<BoxColl>().Depth / 2);
            
        }
        else
            PenetrationDistance.z = 300f;


        AbsPenetrationDistance.x = Math.Abs(PenetrationDistance.x);
        AbsPenetrationDistance.y = Math.Abs(PenetrationDistance.y);
        AbsPenetrationDistance.z = Math.Abs(PenetrationDistance.z);

        //checks the abs of the penetration distance 
        //these if statements check the face it impacted with. 
        if (AbsPenetrationDistance.x < AbsPenetrationDistance.y)
        {
            if (AbsPenetrationDistance.x < AbsPenetrationDistance.z)
            {
  
                GenerateCorrection(Box1, Box2, PenetrationDistance.x);
                if (Box1.GetComponent<RigidBody>().UseGravity)
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        if (Box1.GetComponent<RigidBody>().UseGravity)
                        {
                            Vector3 NewVelocity1;
                            Vector3 NewVelocity2;
                            NewVelocity1 = Box1.GetComponent<RigidBody>().Velocity;
                            NewVelocity1 += Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity1 -= Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);
                            NewVelocity2 = Box2.GetComponent<RigidBody>().Velocity;
                            NewVelocity2 += Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity2 -= Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);

                            Box2.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity2.x, Box2.GetComponent<RigidBody>().Velocity.y, NewVelocity2.z);
                            Box1.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity1.x, Box1.GetComponent<RigidBody>().Velocity.y, NewVelocity1.z);
                        }
                        else
                        {
                            Vector3 Velocity;
                            Vector3 reflectedVelocity;
                            Velocity = Box2.GetComponent<RigidBody>().Velocity;


                            reflectedVelocity.x = Box2.GetComponent<RigidBody>().Velocity.x - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.x));
                            Box2.GetComponent<RigidBody>().Velocity.x = reflectedVelocity.x;
                        }
                    }
                    else
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box1.GetComponent<RigidBody>().Velocity;


                        reflectedVelocity.x = Box1.GetComponent<RigidBody>().Velocity.x - (Box1.GetComponent<RigidBody>().Bounce * (Velocity.x));
                        Box1.GetComponent<RigidBody>().Velocity.x = reflectedVelocity.x;
                    }

                }
                else
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box2.GetComponent<RigidBody>().Velocity;


                        reflectedVelocity.x = Box2.GetComponent<RigidBody>().Velocity.x - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.x));
                        Box2.GetComponent<RigidBody>().Velocity.x = reflectedVelocity.x;
                    }
                }
            }
            else
            {
                GenerateCorrection(Box1, Box2, PenetrationDistance.z);
                if (Box1.GetComponent<RigidBody>().UseGravity)
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        if (Box1.GetComponent<RigidBody>().UseGravity)
                        {
                            Vector3 NewVelocity1;
                            Vector3 NewVelocity2;
                            NewVelocity1 = Box1.GetComponent<RigidBody>().Velocity;
                            NewVelocity1 += Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity1 -= Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);
                            NewVelocity2 = Box2.GetComponent<RigidBody>().Velocity;
                            NewVelocity2 += Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity2 -= Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);

                            Box2.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity2.x, Box2.GetComponent<RigidBody>().Velocity.y, NewVelocity2.z);
                            Box1.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity1.x, Box1.GetComponent<RigidBody>().Velocity.y, NewVelocity1.z);
                        }
                        else
                        {
                            Vector3 Velocity;
                            Vector3 reflectedVelocity;
                            Velocity = Box2.GetComponent<RigidBody>().Velocity;

                            reflectedVelocity.z = Box2.GetComponent<RigidBody>().Velocity.z - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.z));
                            Box2.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                        }
                    }
                    else
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box1.GetComponent<RigidBody>().Velocity;

                        reflectedVelocity.z = Box1.GetComponent<RigidBody>().Velocity.z - (Box1.GetComponent<RigidBody>().Bounce * (Velocity.z));
                        Box1.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                    }

                }
                else
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box2.GetComponent<RigidBody>().Velocity;

                        reflectedVelocity.z = Box2.GetComponent<RigidBody>().Velocity.z - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.z));
                        Box2.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                    }
                }
            }
        }
        else
        {
            if (AbsPenetrationDistance.y < AbsPenetrationDistance.z)
            {
                GenerateCorrection(Box1, Box2, PenetrationDistance.y);
                Vector3 Velocity;
                Vector3 reflectedVelocity;


                Velocity = Box1.GetComponent<RigidBody>().Velocity;

                reflectedVelocity.y = Box1.GetComponent<RigidBody>().Velocity.y - (Box1.GetComponent<RigidBody>().Bounce * (Velocity.y));
                Box1.GetComponent<RigidBody>().Velocity.y = reflectedVelocity.y;

                if (Box1.transform.position.y > Box2.transform.position.y && Box2.GetComponent<RigidBody>().IsGrounded)
                {
                    Box1.GetComponent<RigidBody>().Force -= Box1.GetComponent<RigidBody>().Gravity;
                }

                Velocity = Box2.GetComponent<RigidBody>().Velocity;

                reflectedVelocity.y = Box2.GetComponent<RigidBody>().Velocity.y - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.y));
                Box2.GetComponent<RigidBody>().Velocity.y = reflectedVelocity.y;

                if (Box2.transform.position.y > Box1.transform.position.y && Box1.GetComponent<RigidBody>().IsGrounded)
                {
                    Box2.GetComponent<RigidBody>().Force -= Box2.GetComponent<RigidBody>().Gravity;

                }
            }
            else if (AbsPenetrationDistance.y > AbsPenetrationDistance.z)
            {
                GenerateCorrection(Box1, Box2, PenetrationDistance.z);
                if (Box1.GetComponent<RigidBody>().UseGravity)
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        if (Box1.GetComponent<RigidBody>().UseGravity)
                        {
                            Vector3 NewVelocity1;
                            Vector3 NewVelocity2;
                            NewVelocity1 = Box1.GetComponent<RigidBody>().Velocity;
                            NewVelocity1 += Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity1 -= Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);
                            NewVelocity2 = Box2.GetComponent<RigidBody>().Velocity;
                            NewVelocity2 += Vector3.Project(Box1.GetComponent<RigidBody>().Velocity, Box2.transform.position - Box1.transform.position);
                            NewVelocity2 -= Vector3.Project(Box2.GetComponent<RigidBody>().Velocity, Box1.transform.position - Box2.transform.position);

                            Box2.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity2.x, Box2.GetComponent<RigidBody>().Velocity.y, NewVelocity2.z);
                            Box1.GetComponent<RigidBody>().Velocity = new Vector3(NewVelocity1.x, Box1.GetComponent<RigidBody>().Velocity.y, NewVelocity1.z);
                        }
                        else
                        {
                            Vector3 Velocity;
                            Vector3 reflectedVelocity;
                            Velocity = Box2.GetComponent<RigidBody>().Velocity;

                            reflectedVelocity.z = Box2.GetComponent<RigidBody>().Velocity.z - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.z));
                            Box2.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                        }
                    }
                    else
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box2.GetComponent<RigidBody>().Velocity;

                        reflectedVelocity.z = Box2.GetComponent<RigidBody>().Velocity.z - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.z));
                        Box2.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                    }

                }
                else
                {
                    if (Box2.GetComponent<RigidBody>().UseGravity)
                    {
                        Vector3 Velocity;
                        Vector3 reflectedVelocity;
                        Velocity = Box2.GetComponent<RigidBody>().Velocity;

                        reflectedVelocity.z = Box2.GetComponent<RigidBody>().Velocity.z - (Box2.GetComponent<RigidBody>().Bounce * (Velocity.z));
                        Box2.GetComponent<RigidBody>().Velocity.z = reflectedVelocity.z;
                    }
                }
            }
        }
    }
    private void GenerateCorrection(GameObject Box1, GameObject Box, float PenetrationDistance)
    {
        Vector3 Normalized = Vector3.Normalize(Box1.transform.position - new Vector3(Box.transform.position.x, Box1.transform.position.y, Box1.transform.position.z));
        //generates correction for box one and all boxes in the scene
        if (!Box1.GetComponent<RigidBody>().IsGrounded)
        {
            if (!Box.GetComponent<RigidBody>().IsGrounded)
            {
                Box1.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                Box.transform.position += ((0.5f * (PenetrationDistance)) * Normalized);
            }
            else
            {
                if (Math.Abs(Vector3.Magnitude(Box1.GetComponent<RigidBody>().Velocity)) > 0.5f)
                {
                    Box1.transform.position += ((0.5f * (PenetrationDistance)) * -Normalized);
                }

            }
        }
        else
        {
            if (!Box.GetComponent<RigidBody>().IsGrounded)
            {
                if (Math.Abs(Vector3.Magnitude(Box.GetComponent<RigidBody>().Velocity)) > 0.5f)
                {
                    Box.transform.position += (((PenetrationDistance)) * Normalized);
                }
            }
        }
    }
    private void SetIsGrounded(GameObject CurrentObject, bool IsGrounded)
    {
        //sets if the object is grounded or not
        if (IsGrounded)
        {
            CurrentObject.GetComponent<RigidBody>().IsGrounded = true;
        }
        else
        {
            CurrentObject.GetComponent<RigidBody>().IsGrounded = false;
        }

    }
    public static bool CheckForBoxCollision(Vector3 Max, Vector3 Min, GameObject Sphere)
    {
        //sets of if statements to check for minimum and maximum values for collision
        double SetMin = 0;

        if (Sphere.transform.position.x < Min.x)
        {
            SetMin += Math.Pow(Sphere.transform.position.x - Min.x, 2);
        }
        else if (Sphere.transform.position.x > Max.x)
        {
            SetMin += Math.Pow(Sphere.transform.position.x - Max.x, 2);
        }

        if (Sphere.transform.position.y < Min.y)
        {
            SetMin += Math.Pow(Sphere.transform.position.y - Min.y, 2);
        }
        else if (Sphere.transform.position.y > Max.y)
        {
            SetMin += Math.Pow(Sphere.transform.position.y - Max.y, 2);
        }

        if (Sphere.transform.position.z < Min.z)
        {
            SetMin += Math.Pow(Sphere.transform.position.z - Min.z, 2);
        }
        else if (Sphere.transform.position.z > Max.z)
        {
            SetMin += Math.Pow(Sphere.transform.position.z - Max.z, 2);
        }

        return SetMin <= Math.Pow(Sphere.GetComponent<SphereColl>().Radius, 2);

    }
}