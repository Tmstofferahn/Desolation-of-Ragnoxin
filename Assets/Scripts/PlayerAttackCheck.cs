using UnityEngine;
using System.Collections;

public class PlayerAttackCheck : MonoBehaviour{
	public static int meleeDamage = 1;
	public static int bulletDamage = 2;
	int amount = 0;


	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerController.weaponChoice == 0) {
			amount = meleeDamage;
		}
		if (PlayerController.weaponChoice == 1) {
			amount = bulletDamage;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("thing!");

		// if enemy enters collider, start attacking
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage("takeDamage", amount);
		}
	}


}
