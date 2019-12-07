using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFocus : MonoBehaviour
{
    public float Speed = 35.0f;
    private bool IsIsMovingTowardsTarget = false;

    void Start()
    {

    }
    void Update()
    {
        //sets the debug to look at the correct object 
        if (Input.GetButtonDown("Fire3"))
        {
            if (IsIsMovingTowardsTarget == true)
            {
                IsIsMovingTowardsTarget = false;
            }
            else
            {
                IsIsMovingTowardsTarget = true;
            }
        }
        //when you want to see the object you can snap to it 
        if (IsIsMovingTowardsTarget)
            MoveTowards(gameObject.GetComponentInChildren<ItemSelect>().targetObject);

    }
    public void MoveTowards(Vector3 target)
    {
        //transforms the position
        transform.position = Vector3.Lerp(transform.position, target, Speed * Time.deltaTime);

    }    
}