using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public SteamVR_ControllerManager ControllerManager;
    public Gun Gun;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TrackedController controller;
    SteamVR_Controller.Device device;

	void Awake ()
    {
        trackedObj = ControllerManager.right.GetComponent<SteamVR_TrackedObject>();
        controller = ControllerManager.right.GetComponent<SteamVR_TrackedController>();
	}

    void Start()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void OnEnable()
    {
        controller.TriggerClicked += Gun.Shoot;
    }
}
