using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalCamera : MonoBehaviour {

	public float AngleIncrement = 0.25f;

	public Transform Trans
	{
		get 
		{
			if (trans == null)
				trans = GetComponent<Transform>();

			return trans;
		}
	}

	Transform trans;
	float angle = 0;


	void Update () 
	{
		Trans.RotateAround(Vector3.zero, Vector3.up, AngleIncrement);
		Trans.LookAt(Vector3.zero);
	}
}
