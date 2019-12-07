using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    Vector2 MouseLook;
    Vector2 Smoothing;
    public float Sensitivity = 5.0f;
    public float Smoothsteps = 2.0f;

    GameObject character;

    void Start()
    {
        //transforms the character
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        //allows the mouse to aim for the user
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(Sensitivity * Smoothsteps, Sensitivity * Smoothsteps));
        Smoothing.x = Mathf.Lerp(Smoothing.x, md.x, 1f / Smoothsteps);
        Smoothing.y = Mathf.Lerp(Smoothing.y, md.y, 1f / Smoothsteps);
        MouseLook += Smoothing;

        transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, character.transform.up);
    }
}