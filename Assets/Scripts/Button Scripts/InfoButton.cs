using UnityEngine;
using System.Collections;

public class InfoButton : ButtonController {

	void OnMouseDown()
	{
		source.PlayOneShot (pressed);
		Debug.Log ("To Info");
		Application.LoadLevel (2);
	}
}
