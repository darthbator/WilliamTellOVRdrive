using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Transform apple;
	public Transform head;
    public Transform firingTrans;
    public float minDistance;
    private float distance;

    private bool canFire {
        get { return (distance < minDistance) ? false : true;  }
    }

	
	void Update () {
        distance = Vector3.Distance(firingTrans.position, apple.position);
		if (!canFire)
			Debug.Log("TO CLOSE!!!");
     }

    private void Fire ()
    {
        if (!canFire)
            return;

		Debug.DrawRay(firingTrans.position, firingTrans.forward * 10000f, Color.red, 5f);

        RaycastHit hit;
        if (Physics.Raycast(firingTrans.position, firingTrans.forward, out hit)) {
			if (hit.transform == apple) {
				Debug.Log("Apple core!");
				apple.GetComponent<Renderer>().material.color = Color.green;
			} else if (hit.transform == head)
				Debug.LogError("You shot your son!!!");
				
        }
    }

    public void Shoot(object sender, ClickedEventArgs e)
    {
		Fire();
        PlayerController.Instance.Haptic(1000);
    }
}
