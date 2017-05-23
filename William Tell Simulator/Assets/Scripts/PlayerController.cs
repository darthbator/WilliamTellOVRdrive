using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

	public float endGameWait = 3f;
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
		controller.TriggerClicked += Gun.Shoot;
	}

    void Start()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    public void Haptic(ushort microsecondDuration)
    {
        microsecondDuration = (ushort)Mathf.Clamp(microsecondDuration, 1, 3999);
        device.TriggerHapticPulse(microsecondDuration);
    }

	public void EndGame () {
		StartCoroutine(_EndGame());
	}

	private IEnumerator _EndGame () {
		yield return new WaitForSeconds (endGameWait);
		SceneManager.LoadScene(0);
	}
}
