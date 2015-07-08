using UnityEngine;
using System.Collections;

public class Quit_ButtonController : ButtonController {

	void OnMouseDown()
	{
		source.PlayOneShot (pressed);
		Debug.Log ("Starting Level 1");

#if UNITY_EDITOR
		// Application.Quit doesn't work in the editor, so the next line closes the game there instead
		// Now allows the game to be built while maintaning editor commands
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
