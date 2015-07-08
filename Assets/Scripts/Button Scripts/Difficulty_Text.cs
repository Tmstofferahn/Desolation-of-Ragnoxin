using UnityEngine;
using System.Collections;

public class Difficulty_Text : MonoBehaviour {
	public static int difficulty = 4;
	Rect rex = new Rect(Screen.width/2 - 10, Screen.height/2 - 70, 20, 20);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.TextArea(rex, difficulty.ToString());
	}
}
