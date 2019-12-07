using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    [SerializeField]
    private ItemSelect selected;
    [SerializeField]
    private Text ObjectNameText;
    [SerializeField]
    private Text VelocityText;
    [SerializeField]
    private Text ForceText;
    [SerializeField]
    private Text AccelerationText;
    [SerializeField]
    private Text positionText;

    void Start()
    {
        
    }

    void Update()
    {
        if (selected.isSelected)
        {
            //converts values to strings
            ObjectNameText.text = selected.Objectname;
            VelocityText.text = selected.Vel.ToString("F3");
            ForceText.text = selected.Forces.ToString("F3");
            AccelerationText.text = selected.Accel.ToString("F3");
            positionText.text = selected.Pos.ToString("F3");
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        //shows no values when reset
        ObjectNameText.text = "None Selected";
        VelocityText.text = Vector3.zero.ToString("F3");
        ForceText.text = Vector3.zero.ToString("F3");
        AccelerationText.text = Vector3.zero.ToString("F3");
        positionText.text = Vector3.zero.ToString("F3");
    }
}
    