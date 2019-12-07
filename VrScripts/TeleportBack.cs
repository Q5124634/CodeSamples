using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBack : MonoBehaviour {
    public GameObject CameraRig;
    public GameObject ReturnStart;
    // Use this for initialization
    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.transform.CompareTag("Controller"))
        {
            CameraRig.transform.position = ReturnStart.transform.position;
        }
    }
}
