using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {

	static public Party TotallyNecessarySingletonInstance;

	public GameObject BalloonPrefab;
	public int BalloonSpawnAmount = 20;
	public float BalloonLifeTime = 15f;

	BoxCollider box;

    void OnDrawGizmosSelected()
    {
        if (box == null)
            box = GetComponent<BoxCollider>();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, box.size);
    }


    void Awake ()
	{
		if (TotallyNecessarySingletonInstance == null)
			TotallyNecessarySingletonInstance = this;
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void OnDestroy()
	{
		TotallyNecessarySingletonInstance = null;
	}

	public void FuckinPartyDude()
	{
		for (int i = 0; i < BalloonSpawnAmount; i++)
		{
			Vector3 RandomSpawnPoint = RandomPointInsideBox();
			Valve.VR.InteractionSystem.Balloon Balloon = GameObject.Instantiate(BalloonPrefab, RandomSpawnPoint, Quaternion.identity).GetComponent<Valve.VR.InteractionSystem.Balloon>();
			Balloon.SetColor(Valve.VR.InteractionSystem.Balloon.BalloonColor.Random);
			Destroy(Balloon.gameObject, BalloonLifeTime);
		}	
	}

	Vector3 RandomPointInsideBox()
	{
		if (box == null)
			box = GetComponent<BoxCollider>();

		float minX = box.bounds.min.x, maxX = box.bounds.max.x;
		float minY = box.bounds.min.y, maxY = box.bounds.max.y;
		float minZ = box.bounds.min.z, maxZ = box.bounds.max.z;

		return new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY), Random.Range(minZ,maxZ));
	}

	#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			FuckinPartyDude();
	}
	#endif
}
