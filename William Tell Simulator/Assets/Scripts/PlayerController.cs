using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public SteamVR_ControllerManager ControllerManager;
    public Gun Gun;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TrackedController controller;
    SteamVR_Controller.Device device;

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
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void Assign ()
    {
        controller.TriggerClicked += Gun.Shoot;
    }

    public void Haptic(ushort microsecondDuration)
    {
        microsecondDuration = (ushort)Mathf.Clamp(microsecondDuration, 1, 3999);
        device.TriggerHapticPulse(microsecondDuration);
    }
}
