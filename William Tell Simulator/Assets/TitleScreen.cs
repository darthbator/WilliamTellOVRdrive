using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

	public float WaitToFadeTime = 3f;
	public float FadeTime = 3f;

	RawImage sickPic;

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
			sickPic.color = new Color(1,1,1, Mathf.Lerp(0,1, timer/FadeTime));

			timer += Time.deltaTime;
			yield return null;
		}

		// wait before fade out
		yield return new WaitForSeconds(WaitToFadeTime);

		// fade out
		timer = FadeTime;
		while (timer > 0.0f)
		{
			sickPic.color = new Color(1,1,1, Mathf.Lerp(0, 1, timer / FadeTime));
			timer -= Time.deltaTime;
			yield return null;
		}
		sickPic.color = new Color(1,1,1,0);

		// done
		yield break;
	}
}
