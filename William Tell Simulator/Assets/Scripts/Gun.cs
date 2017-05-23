using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour {
	private Arrow arrow;

	//SHOTS SHOTS SHOTS SHOTS!!
	public int maxShots;
	private int shots = 0;
	public int Shots {
		get { return shots; }
		set { 
			shots = value;
			if (shots >= maxShots)
				PlayerController.Instance.EndGame(false);
		}
	}

	public Arrow arrowPrefab;
	public float arrowReleaseVelocity;
	public Animator animator;

	public Transform apple;
	public Transform head;
    public Transform firingTrans;
	public Transform arrowFather;
    public float minDistance;
    private float distance;

	public LineRenderer laserSight;

    private bool canFire {
        get { return (distance < minDistance || arrow == null) ? false : true;  }
    }

	void Update () {
        distance = Vector3.Distance(firingTrans.position, apple.position);

		if (!PlayerController.Instance.gameOver) {
			PlayerController.Instance.vrInstructionText.enabled = !canFire;
			PlayerController.Instance.externalMonitorText.enabled = !canFire;
		}

		//Lazerz dawgz
		LaserSight();
     }

	//Old gun firing method goes to sleep... zzz mr gun method!
    /*private void Fire ()
    {
        if (!canFire)
            return;

		Debug.DrawRay(firingTrans.position, firingTrans.forward * 10000f, Color.red, 5f);

		//FIXME model color changes not working after models integrated
        RaycastHit hit;
        if (Physics.Raycast(firingTrans.position, firingTrans.forward, out hit)) {
			if (hit.transform == apple) {
				Debug.Log("Apple core!");
				PlayerController.Instance.vrInstructionText.text = "HE SHOT THE APPLE!!!";
				PlayerController.Instance.externalMonitorText.text = "YOU SHOT THE APPLE!!!!";
				PlayerController.Instance.vrInstructionText.enabled = true;
				PlayerController.Instance.externalMonitorText.enabled = true;
			} else if (hit.transform == head) {
				Debug.LogError("You shot your son!!!");
				PlayerController.Instance.vrInstructionText.text = "YOU'RE DEAD!!!!";
				PlayerController.Instance.externalMonitorText.text = "YOU SHOT HIM YOU MONSTER!!!!!!";
				PlayerController.Instance.vrInstructionText.enabled = true;
				PlayerController.Instance.externalMonitorText.enabled = true;
			}
			PlayerController.Instance.EndGame();
        }
    }*/

	private void ShootArrow () {
		Shots++;
		animator.Play("Shoot");
		arrow.shaftRB.isKinematic = false;
		arrow.shaftRB.useGravity = true;
		arrow.shaftRB.transform.GetComponent<BoxCollider>().enabled = true;

		arrow.arrowHeadRB.isKinematic = false;
		arrow.arrowHeadRB.useGravity = true;
		arrow.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;

		arrow.ArrowReleased(arrowReleaseVelocity);

		arrow.arrowHeadRB.AddForce(arrow.transform.forward * arrowReleaseVelocity, ForceMode.VelocityChange);
		arrow.arrowHeadRB.AddTorque(arrow.transform.forward * 10f);
		arrow = null;
	}

	private void LaserSight () {
		laserSight.SetPosition(0, firingTrans.position);
		RaycastHit hit;
		Vector3 endPos = (Physics.Raycast(firingTrans.position, firingTrans.forward, out hit)) ? hit.point : firingTrans.forward * 1000f;
		laserSight.SetPosition(1, endPos);
	}

    public void Shoot(object sender, ClickedEventArgs e)
    {
		if (PlayerController.Instance.gameOver || !canFire)
			return;

		//Fire();
		ShootArrow();
        PlayerController.Instance.Haptic(1000);
    }

	public void Reload () {
		arrow = Instantiate(arrowPrefab);
		arrow.transform.SetParent(arrowFather, false);
	}
}
