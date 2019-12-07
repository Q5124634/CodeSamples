using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour {

    public string Objectname;
    public Vector3 Vel;
    public Vector3 Forces;
    public Vector3 Accel;
    public Vector3 Pos;
    public Vector3 targetObject;
    private RigidBody selectedBody;
    public bool isSelected;

    void Start()
    {

    }
	
	// Update is called once per frame  
	private void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out Hit, 100.0f))
            {
                //sends out a raycast to see what item has been selected
                selectedBody = Hit.collider.GetComponent<RigidBody>();
                isSelected = true;


            }
            else
            {
                selectedBody = null;
                isSelected = false;

            } 
         }

        if (selectedBody != null && selectedBody.transform.Find("FocusPoint") != null)
        {
            //relays the information on the object
            targetObject = selectedBody.transform.Find("FocusPoint").position;
            Objectname = (selectedBody.transform.gameObject.tag);
            Vel = (selectedBody.transform.gameObject.GetComponent<RigidBody>().Velocity);
            Forces = (selectedBody.transform.gameObject.GetComponent<RigidBody>().Force);
            Accel = (selectedBody.transform.gameObject.GetComponent<RigidBody>().LastAcceleration);
            Pos = (selectedBody.transform.position);

        }

    }
}
