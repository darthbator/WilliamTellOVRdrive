using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

	public bool gameOver = false;
	public float endGameWait = 3f;
    public SteamVR_ControllerManager ControllerManager;
    public Gun Gun;
	public Text vrInstructionText;
	public Text externalMonitorText;

	public int maxHeadHits;
	private int headHits;
	public int HeadHits {
		get { return headHits; }
		set {
			headHits = value;
			if (headHits >= maxHeadHits)
				EndGame(false);
		}
	}

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
        microsecondDuration = (ushort)Mathf.Clamp((int)microsecondDuration, 1, 3999);
        device.TriggerHapticPulse(microsecondDuration);
    }

	public void EndGame (bool winState) {
		StartCoroutine(_EndGame(winState));
	}

	private IEnumerator _EndGame (bool winState) {
		gameOver = true;

		if (winState) {
			vrInstructionText.text = "you shot dat arrow BOIEEEE";
			externalMonitorText.text = "you shot dat arrow BOIEEEE";
		} else {
			PlayerController.Instance.vrInstructionText.text = "YOU'RE DEAD!!!!";
			PlayerController.Instance.externalMonitorText.text = "YOU SHOT HIM YOU MONSTER!!!!!!";
		}

		vrInstructionText.enabled = true;
		externalMonitorText.enabled = true;
		Debug.LogWarning("Restarting game");
		yield return new WaitForSeconds (endGameWait);
		SceneManager.LoadScene(0);
	}
}
