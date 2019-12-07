using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    public GameObject SetCanvas;
    public bool IsCollided;
    // Use this for initialization
    void Start()
    {
        SetCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Controller"))
        {

            SetCanvas.SetActive(true);
            IsCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SetCanvas.SetActive(false);
        IsCollided = false;
    }

    //on button press trigger
    //private void OnTriggerExit(Collider other)
    //{
    //    if (IsCollided)
    //    {
    //        change scene
    //    }
    //}
}
