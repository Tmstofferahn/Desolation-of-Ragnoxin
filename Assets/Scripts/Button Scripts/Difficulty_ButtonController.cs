using UnityEngine;
using System.Collections;

public class Difficulty_ButtonController : ButtonController {
	private static int difficulty = 4, MAX = 1, MIN = 6; // reversed for setting value purposes
	private static int difficulty_display = 4; // Number that is displayed to player 
	public int button;// button value is assigned in Unity. Increase is 1, decrease is -1;\

	
	void OnMouseDown()
	{
		difficulty -= button;
		difficulty_display += button;
		if (difficulty_display > MIN) {
			difficulty_display = MIN;
			difficulty = MAX;
		} else if (difficulty_display < MAX) {
			difficulty_display = MAX;
			difficulty = MIN;
		} else {
			PlayerController.MAX_HEALTH = difficulty;
			PlayerController.MAX_LIVES = difficulty;
			PlayerController.health = difficulty;
			PlayerController.lives = difficulty;

			// Update script that manages difficulty text.
			//This is necessary because two different buttons use this script and were writing two texts to the screen
			Difficulty_Text.difficulty = Difficulty_ButtonController.difficulty_display; 
			source.PlayOneShot (pressed);
			Debug.Log ("Difficulty changed by" + button.ToString ());
		}
	}

}
