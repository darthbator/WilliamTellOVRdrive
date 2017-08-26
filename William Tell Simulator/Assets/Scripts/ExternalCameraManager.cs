using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalCameraManager : MonoBehaviour {

    public static ExternalCameraManager Instance;

    // References
    public Canvas ExternalMonitorCanvas;
    public SteamVR_ControllerManager ControllerManager;
    public Camera[] Cameras;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TrackedController controller;

    int currentCamIndex = 0;


    void Awake()
    {
        trackedObj = ControllerManager.right.GetComponent<SteamVR_TrackedObject>();
        controller = ControllerManager.right.GetComponent<SteamVR_TrackedController>();
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        trackedObj = ControllerManager.right.GetComponent<SteamVR_TrackedObject>();
        controller = ControllerManager.right.GetComponent<SteamVR_TrackedController>();
    }

    void Start()
    {
        controller.PadClicked += PadClicked;

        // Enable Cameras[0] and disable all other cameras
        for (int i = 0; i < Cameras.Length; i++)
            Cameras[i].enabled = (i == 0);
    }

    public void PadClicked(object sender, ClickedEventArgs e)
    {
        // Get X-axis value of touchpad. If player clicks right, then go to next camera. If player clicks left, then go to previous camera.
        Vector2 touchpad = SteamVR_Controller.Input((int)trackedObj.index).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        NextCamera(touchpad.x < 0);
    }

    void NextCamera(bool previous)
    {
        Cameras[currentCamIndex].enabled = false;

        currentCamIndex += (previous ? -1 : 1);
        if (currentCamIndex >= Cameras.Length)
            currentCamIndex = 0;
        else if (currentCamIndex < 0)
            currentCamIndex = Cameras.Length - 1;

        Cameras[currentCamIndex].enabled = true;
        ExternalMonitorCanvas.worldCamera = Cameras[currentCamIndex];
    }
}
