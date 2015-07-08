using UnityEngine;
using System.Collections;

public class Back_ButtonController : ButtonController {

	void OnMouseDown()
	{
		source.PlayOneShot (pressed);
		if (Application.loadedLevel == 0) {
			Application.LoadLevel (1);
			Debug.Log ("To Options");
		} else {
			Debug.Log ("Back to Start");
			Application.LoadLevel (0);
		}
	}
}
