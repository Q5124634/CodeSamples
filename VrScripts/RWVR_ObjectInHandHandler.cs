using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_ObjectInHandHandler : RWVR_InteractionObject
{
    [Header("In game prefab")]
    public GameObject SetPrefabAk47;
    public GameObject SetPrefabOldAk47;
    public GameObject SetPrefabUmp;
    public GameObject SetPrefabSkorp;
    public GameObject SetPrefabM4a1;
    public GameObject SetPrefabPistol;
    public GameObject SetPrefabBullp;
    public GameObject SetPrefabThrowingAxe;
    public GameObject SetPrefabUmpACOG;
    public GameObject SetPrefabM4a1ACOG;
    public GameObject SetPrefabBullpACOG;
    public GameObject SetPrefabAk47ACOG;

    [Header("In game Magazine")]
    public GameObject SetMagazineAk47;
    public GameObject SetMagazineOldAk47;
    public GameObject SetMagazineUmp;
    public GameObject SetMagazineSkorp;
    public GameObject SetMagazineM4a1;
    public GameObject SetMagazinePistol;
    public GameObject SetMagazineBullp;
    public GameObject SetMagazineUmpACOG;
    public GameObject SetMagazineM4a1ACOG;
    public GameObject SetMagazineBullpACOG;
    public GameObject SetMagazineAk47ACOG;

    [Header("In game Camera")]
    public GameObject SetCameraUmpACOG;
    public GameObject SetCameraM4a1ACOG;
    public GameObject SetCameraBullpACOG;
    public GameObject SetCameraAk47ACOG;

    [Header("In game bullet emitter")]
    public GameObject SetAk47Emitter;
    public GameObject SetOldAk47Emitter;
    public GameObject SetUmpEmitter;
    public GameObject SetSkorpEmitter;
    public GameObject SetM4a1Emitter;
    public GameObject SetPistolEmitter;
    public GameObject SetBullpEmitter;
    public GameObject SetUmpACOGEmitter;
    public GameObject SetM4a1ACOGEmitter;
    public GameObject SetBullpACOGEmitter;
    public GameObject SetAk47ACOGEmitter;

    [Header("In game gun canvas")]
    public GameObject SetAk47Canvas;
    public GameObject SetOldAk47Canvas;
    public GameObject SetUmpCanvas;
    public GameObject SetSkorpCanvas;
    public GameObject SetM4a1Canvas;
    public GameObject SetPistolCanvas;
    public GameObject SetBullpCanvas;
    public GameObject SetUmpACOGCanvas;
    public GameObject SetM4a1ACOGCanvas;
    public GameObject SetBullpACOGCanvas;
    public GameObject SetAk47ACOGCanvas;

    [Header("In game gun Bools")]
    public bool SetBoolPrefabAk47 = false;
    public bool SetBoolPrefabOldAk47 = false;
    public bool SetBoolPrefabUmp = false;
    public bool SetBoolPrefabSkorp = false;
    public bool SetBoolPrefabM4a1 = false;
    public bool SetBoolPrefabPistol = false;
    public bool SetBoolPrefabBullp = false;
    public bool SetBoolPrefabUmpACOG = false;
    public bool SetBoolPrefabM4a1ACOG = false;
    public bool SetBoolPrefabBullpACOG = false;
    public bool SetBoolPrefabAk47ACOG = false;
    public bool SetBoolPrefabThrowingAxe = true;

    public void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            SetBoolPrefabBullp = true;
            SetBoolPrefabThrowingAxe = false;
            //SpawnObjectInHand(SetPrefabBullp, controller);
            SetCameras();
            SetBullpEmitter.SetActive(true);
            SetBullpCanvas.SetActive(true);
            SetMagazineBullp.SetActive(true);
        }

        if (Input.GetKeyDown("w"))
        {
            SetBoolPrefabBullpACOG = true;
            SetBoolPrefabThrowingAxe = false;
           // SpawnObjectInHand(SetPrefabBullpACOG, controller);
            SetCameraBullpACOG.SetActive(true);
            SetBullpACOGEmitter.SetActive(true);
            SetBullpACOGCanvas.SetActive(true);
            SetMagazineBullpACOG.SetActive(true);
        }

        if (Input.GetKeyDown("p"))
        {
            SetBoolPrefabAk47 = false;
            SetBoolPrefabOldAk47 = false;
            SetBoolPrefabUmp = false;
            SetBoolPrefabSkorp = false;
            SetBoolPrefabM4a1 = false;
            SetBoolPrefabPistol = false;
            SetBoolPrefabBullp = false;
            SetBoolPrefabUmpACOG = false;
            SetBoolPrefabM4a1ACOG = false;
            SetBoolPrefabBullpACOG = false;
            SetBoolPrefabAk47ACOG = false;
            SetBoolPrefabThrowingAxe = true;
            SetCameras();
            SetEmmiter();
            SetCanvas();
            SetMagazines();
        }
    }

    public void Start()
    {
        SetCameras();
        SetEmmiter();
        SetCanvas();
        SetMagazines();
    }

    public List<GameObject> randomPrefabs = new List<GameObject>();
    private void SpawnObjectInHand(GameObject prefab, RWVR_InteractionController controller)
    {
        GameObject spawnedObject = Instantiate(prefab, controller.snapColliderOrigin.position, controller.transform.rotation);
        controller.SwitchInteractionObjectTo(spawnedObject.GetComponent<RWVR_InteractionObject>());
        OnTriggerWasReleased(controller);
    }


    public override void OnTriggerWasPressed(RWVR_InteractionController controller)
    {
        base.OnTriggerWasPressed(controller);

        if (RWVR_ControllerManager.Instance.Gun1)
        {
            SetBoolPrefabAk47 = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabAk47, controller);
            SetCameras();
            SetAk47Emitter.SetActive(true);
            SetAk47Canvas.SetActive(true);
            SetMagazineAk47.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun2)
        {
            SetBoolPrefabOldAk47 = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabOldAk47, controller);
            SetCameras();
            SetOldAk47Emitter.SetActive(true);
            SetOldAk47Canvas.SetActive(true);
            SetMagazineOldAk47.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun3)
        {
            SetBoolPrefabUmp = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabUmp, controller);
            SetCameras();
            SetUmpEmitter.SetActive(true);
            SetUmpCanvas.SetActive(true);
            SetMagazineUmp.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun4)
        {
            SetBoolPrefabSkorp = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabSkorp, controller);
            SetCameras();
            SetSkorpEmitter.SetActive(true);
            SetSkorpCanvas.SetActive(true);
            SetMagazineSkorp.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun5)
        {
            SetBoolPrefabM4a1 = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabM4a1, controller);
            SetCameras();
            SetM4a1Emitter.SetActive(true);
            SetM4a1Canvas.SetActive(true);
            SetMagazineM4a1.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun6)
        {
            SetBoolPrefabPistol = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabPistol, controller);
            SetCameras();
            SetPistolEmitter.SetActive(true);
            SetPistolCanvas.SetActive(true);
            SetMagazinePistol.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun7)
        {
            SetBoolPrefabBullp = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabBullp, controller);
            SetCameras();
            SetBullpEmitter.SetActive(true);
            SetBullpCanvas.SetActive(true);
            SetMagazineBullp.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun8)
        {

            SetBoolPrefabUmpACOG = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabUmpACOG, controller);
            SetCameraUmpACOG.SetActive(true);
            SetUmpACOGEmitter.SetActive(true);
            SetUmpACOGCanvas.SetActive(true);
            SetMagazineUmpACOG.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun9)
        {
            SetBoolPrefabM4a1ACOG = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabM4a1ACOG, controller);
            SetCameraM4a1ACOG.SetActive(true);
            SetM4a1ACOGEmitter.SetActive(true);
            SetM4a1ACOGCanvas.SetActive(true);
            SetMagazineM4a1ACOG.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun10)
        {
            SetBoolPrefabBullpACOG = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabBullpACOG, controller);
            SetCameraBullpACOG.SetActive(true);
            SetBullpACOGEmitter.SetActive(true);
            SetBullpACOGCanvas.SetActive(true);
            SetMagazineBullpACOG.SetActive(true);
        }

        if (RWVR_ControllerManager.Instance.Gun11)
        {
            SetBoolPrefabAk47ACOG = true;
            SetBoolPrefabThrowingAxe = false;
            SpawnObjectInHand(SetPrefabAk47ACOG, controller);
            SetCameraAk47ACOG.SetActive(true);
            SetAk47ACOGEmitter.SetActive(true);
            SetAk47ACOGCanvas.SetActive(true);
            SetMagazineAk47ACOG.SetActive(true);
        }

        else
        {
            SetBoolPrefabAk47 = false;
            SetBoolPrefabOldAk47 = false;
            SetBoolPrefabUmp = false;
            SetBoolPrefabSkorp = false;
            SetBoolPrefabM4a1 = false;
            SetBoolPrefabPistol = false;
            SetBoolPrefabBullp = false;
            SetBoolPrefabUmpACOG = false;
            SetBoolPrefabM4a1ACOG = false;
            SetBoolPrefabBullpACOG = false;
            SetBoolPrefabAk47ACOG = false;
            SetBoolPrefabThrowingAxe = true;

            SpawnObjectInHand(SetPrefabThrowingAxe, controller);

            SetCameras();
            SetEmmiter();
            SetCanvas();
            SetMagazines();
        }
    }
    private void SetCameras()
    {
     SetCameraUmpACOG.SetActive(false);
    SetCameraM4a1ACOG.SetActive(false);
     SetCameraBullpACOG.SetActive(false);
    SetCameraAk47ACOG.SetActive(false);
    }
    private void SetCanvas()
    {
        SetAk47Canvas.SetActive(false);
        SetOldAk47Canvas.SetActive(false);
        SetUmpCanvas.SetActive(false);
        SetSkorpCanvas.SetActive(false);
        SetM4a1Canvas.SetActive(false);
        SetPistolCanvas.SetActive(false);
        SetBullpCanvas.SetActive(false);
        SetUmpACOGCanvas.SetActive(false);
        SetM4a1ACOGCanvas.SetActive(false);
        SetBullpACOGCanvas.SetActive(false);
        SetAk47ACOGCanvas.SetActive(false);
    }
    private void SetEmmiter()
    {
        SetAk47Emitter.SetActive(false);
        SetOldAk47Emitter.SetActive(false);
        SetUmpEmitter.SetActive(false);
        SetSkorpEmitter.SetActive(false);
        SetM4a1Emitter.SetActive(false);
        SetPistolEmitter.SetActive(false);
        SetBullpEmitter.SetActive(false);
        SetUmpACOGEmitter.SetActive(false);
        SetM4a1ACOGEmitter.SetActive(false);
        SetBullpACOGEmitter.SetActive(false);
        SetAk47ACOGEmitter.SetActive(false);
    }
    private void SetMagazines()
    {
        SetMagazineAk47.SetActive(false);
        SetMagazineOldAk47.SetActive(false);
        SetMagazineUmp.SetActive(false);
        SetMagazineSkorp.SetActive(false);
        SetMagazineM4a1.SetActive(false);
        SetMagazinePistol.SetActive(false);
        SetMagazineBullp.SetActive(false);
        SetMagazineUmpACOG.SetActive(false);
        SetMagazineM4a1ACOG.SetActive(false);
        SetMagazineBullpACOG.SetActive(false);
        SetMagazineAk47ACOG.SetActive(false);
    }

}


