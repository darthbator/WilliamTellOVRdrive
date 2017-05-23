using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Transform apple;
	public Transform head;
    public Transform firingTrans;
    public float minDistance;
    private float distance;

	public LineRenderer laserSight;

    private bool canFire {
        get { return (distance < minDistance) ? false : true;  }
    }
	
	void Update () {
        distance = Vector3.Distance(firingTrans.position, apple.position);
		PlayerController.Instance.vrInstructionText.enabled = !canFire;

		//Lazerz dawgz
		LaserSight();
     }

    private void Fire ()
    {
        if (!canFire)
            return;

		Debug.DrawRay(firingTrans.position, firingTrans.forward * 10000f, Color.red, 5f);

		//FIXME model color changes not working after models integrated
        RaycastHit hit;
        if (Physics.Raycast(firingTrans.position, firingTrans.forward, out hit)) {
			if (hit.transform == apple) {
				Debug.Log("Apple core!");
				//apple.GetComponent<Renderer>().material.color = Color.green;
			} else if (hit.transform == head) {
				Debug.LogError("You shot your son!!!");
				/*foreach (Material mat in head.GetComponent<Renderer>().materials)
					mat.color = Color.red;*/
			}

			PlayerController.Instance.EndGame();
        }
    }

	private void LaserSight () {
		laserSight.SetPosition(0, firingTrans.position);
		RaycastHit hit;
		Vector3 endPos = (Physics.Raycast(firingTrans.position, firingTrans.forward, out hit)) ? hit.point : firingTrans.forward * 1000f;
		laserSight.SetPosition(1, endPos);
	}

    public void Shoot(object sender, ClickedEventArgs e)
    {
		Fire();
        PlayerController.Instance.Haptic(1000);
    }
}
