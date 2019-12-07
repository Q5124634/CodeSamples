using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLaunch : MonoBehaviour {

    public GameObject Sphere;
    public GameObject LaunchPoint;
    public float AddedAcc;
 
    void Start()
    {

    }

	void FixedUpdate() {

        //when held it adds to the acceleration of the ball
        if (Input.GetButton("Fire1"))
        {
            AddedAcc = AddedAcc + 0.2f;
        }

        //once released it shoots the ball at the current stored velocity in the direction the camera is looking
        if (Input.GetButtonUp("Fire1"))
        {
            GameObject Sphere1 = Instantiate(Sphere, LaunchPoint.transform.position, LaunchPoint.transform.rotation);
            Sphere1.GetComponent<RigidBody>().Velocity = LaunchPoint.transform.forward * AddedAcc;
            AddedAcc = 0;
        }
    }
}
