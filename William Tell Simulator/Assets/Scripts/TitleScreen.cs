using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TitleScreen : MonoBehaviour {

	public float WaitToFadeTime = 3f;
	public float FadeTime = 3f;

	RawImage sickPic;

	public static event Action OnTitleComplete;

	IEnumerator Start () 
	{
		sickPic = GetComponent<RawImage>();

		// wait before fade in
		sickPic.color = new Color(1,1,1,0);
		yield return new WaitForSeconds(1f);

		// fade in
		float timer = 0;
		while (timer < FadeTime)
		{
			timer += Time.deltaTime;
			sickPic.color = new Color(1,1,1, Mathf.Lerp(0,1, timer/FadeTime));
			yield return null;
		}

		// wait before fade out
		yield return new WaitForSeconds(WaitToFadeTime);

		// fade time divided by 3
		FadeTime /= 3;

		Vector3 startingPos = transform.localPosition;
		Vector3 endingPos = new Vector3(280f, -175f, 0f);
		Vector3 startingScale = transform.localScale;
		// fade out
		timer = FadeTime;
		while (timer > 0.0f)
		{
			//sickPic.color = new Color(1,1,1, Mathf.Lerp(0, 1, timer / FadeTime));
			transform.localPosition = Vector3.Lerp(endingPos, startingPos, timer / FadeTime);
			transform.localScale = Vector3.Lerp(Vector3.one / 10, startingScale, timer / FadeTime);
			timer -= Time.deltaTime;
			yield return null;
		}
		//sickPic.color = new Color(1,1,1,0);
		transform.localPosition = endingPos;
		transform.localScale = Vector3.one / 10;

		if (OnTitleComplete != null)
			OnTitleComplete();

		// done
		yield break;
	}
}
