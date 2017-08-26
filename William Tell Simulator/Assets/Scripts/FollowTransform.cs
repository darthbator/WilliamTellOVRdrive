using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

	public Transform FollowTrans;
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

	void Update () 
	{
		if (FollowTrans == null)
			return;

		Trans.position = FollowTrans.position;
		Trans.rotation = FollowTrans.rotation;
	}
}
