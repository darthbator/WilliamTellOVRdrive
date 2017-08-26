using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Public references: Literally just a singleton class so we can easily store/get references to things.
/// </summary>
public class PublicReferences : MonoBehaviour {

	public static PublicReferences Instance;

	[Header("Player")]
	public Transform PlayerHead;

	[Header("Other")]
	public GameObject BloodHitPrefab;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
