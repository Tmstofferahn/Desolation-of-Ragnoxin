using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
	
	public AudioClip over;
	public AudioClip pressed;
	protected AudioSource source;
	protected bool isOver = false;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
		source.PlayOneShot (pressed);
		Debug.Log ("Starting Level");
		Application.LoadLevel(3);
	}

	void OnMouseOver()
	{
		if (!isOver){
			source.PlayOneShot (over);
			isOver = true;
		}
	}

	void OnMouseExit()
	{
		isOver = false;
	}
}
