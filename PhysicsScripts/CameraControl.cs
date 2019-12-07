using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 20.0f;
    public float JumpHeight = 5f;
    
    void Start()
    {
        //trnsforms the character
        GameObject character = this.transform.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        //allows the player to move and check the scene
        float jump = Input.GetAxis("Jump") * -JumpHeight * 15;
        jump = Input.GetAxis("Jump") * JumpHeight * 15;
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, jump, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}