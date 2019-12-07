using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_SnapToController : RWVR_InteractionObject
{
    public bool hideControllerModel; // 1
    public Vector3 snapPositionOffset; // 2
    public Vector3 snapRotationOffset; // 3

    private Rigidbody rb; // 4

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void ConnectToController(RWVR_InteractionController controller) // 1
    {
        cachedTransform.SetParent(controller.transform); // 2

        cachedTransform.rotation = controller.transform.rotation; // 3
        cachedTransform.Rotate(snapRotationOffset);
        cachedTransform.position = controller.snapColliderOrigin.position; // 4
        cachedTransform.Translate(snapPositionOffset, Space.Self);

        rb.useGravity = false; // 5
        rb.isKinematic = true; // 6
    }

    private void ReleaseFromController(RWVR_InteractionController controller) // 1
    {
        cachedTransform.SetParent(null); // 2

        rb.useGravity = true; // 3
        rb.isKinematic = false;

        rb.velocity = controller.velocity; // 4
        rb.angularVelocity = controller.angularVelocity;
    }

    public override void OnTriggerWasPressed(RWVR_InteractionController controller) // 1
    {
        base.OnTriggerWasPressed(controller); // 2

        if (hideControllerModel) // 3
        {
            controller.HideControllerModel();
        }

        ConnectToController(controller); // 4
    }

    public override void OnTriggerWasReleased(RWVR_InteractionController controller) // 1
    {
        base.OnTriggerWasReleased(controller); // 2

        if (hideControllerModel) // 3
        {
            controller.ShowControllerModel();
        }

        ReleaseFromController(controller); // 4
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
