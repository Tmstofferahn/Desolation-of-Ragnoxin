using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("entered");
            //Application.LoadLevel("Level2");
            Application.LoadLevel(Application.loadedLevel+1);
        }
	
	}
	
}
