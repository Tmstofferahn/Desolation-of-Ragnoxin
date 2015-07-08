using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour 
{
	public float maxSpeed = 8f;
	public int speed;
	public float jumpForceFactor = 2.5f; //in 10,000s
	float jumpForce;
	bool doubleJump = true; //double jump already used = true
	bool grounded = false; //on ground?
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private Animator animator;
	Vector3 playerPos;
	Transform playerTransform;

	bool facingRight = true;

	//variables for sound
	public static AudioClip swing; //stores sound effect. Is input in unity GUI
	public static AudioClip grunt;
	private static AudioSource source;

	// global variables
	public static int MAX_LIVES = 3;
	public static int MAX_HEALTH = 3;
	public static int MAX_AMMO = 20;
	public static int lives = MAX_LIVES;
	public static int health = MAX_HEALTH;
	public static int ammo = MAX_AMMO;
	public static int score = 0;
	public static int weaponChoice = 0;

    void Start() {

        animator = GetComponent<Animator>();
		jumpForce = jumpForceFactor * 10000; //jump multiplier
		playerTransform = GameObject.Find ("Player").transform;
		source = GetComponent<AudioSource>();
    }   

	void Update()
	{
		playerPos = playerTransform.position;
		

		//JUMP CONTROL
		if((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0f); //reset upwards/downwards velocity before jumping (helps with second jump)
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
			if(!doubleJump && !grounded)
			{
				doubleJump = true;
			}
		}

		// Reset Level Key
		if(Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
			health = MAX_HEALTH; //Resets player health with level
		}

		// Fallen out of the world detection
		if (playerPos.y < -15) {
			// reduce health
			takeDamage(PlayerController.health);
		}

		//OLD ANIMATION CODE DELETE IF NO LONGER NEEDED
		//if(Input.GetKey("left")) {
         //   animator.SetBool("moveLeft", true);
        //}
        //if (Input.GetKey("right")) {
          //  animator.SetBool("moveLeft", false);
       // }

	}

	//--New class for damaging player-------------------
	public static void takeDamage(int amount){
		PlayerController.source.PlayOneShot(PlayerController.grunt);
		PlayerController.health--;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        FloatyText.Create("-1", playerObj.transform.position, Vector3.up, Color.red, 1); 
		if(PlayerController.health <= 0) {
			// if dead, reduce lives, reset health and check for 'game over'
			PlayerController.health = MAX_HEALTH;
			PlayerController.lives--;
			// todo: play death sound
			if(PlayerController.lives <= 0) {
				// trigger gameover here once added
				// for now, reverts to level 1
				Application.LoadLevel("Level1");
				// Resets player's lives on game reset.
				PlayerController.lives = MAX_LIVES;
			} else {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	//----------------------------------------------------------

    void FixedUpdate() 
	{

		//check if player is on ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded)
			doubleJump = false; //if player not on ground, give double jump.

		//Uncomment this to make it to where the player cannot turn while in the air.
		//if (!grounded)
			//return;

		//move left and right
		float move = Input.GetAxis ("Horizontal"); //get input direction (+/- value)
		animator.SetFloat ("Speed", Mathf.Abs (move)); //apply movement direction to variable for animation (+/- value)


		//animation for left/right
		if (move > 0 && !facingRight) {
			Flip ();
		}
		else if (move < 0 && facingRight) {
			Flip ();
		}

		//Change weapon
		// 0 - Melee
		// 1 - Gun
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			weaponChoice = 0;
			animator.SetInteger("Weapon", 0); 
		}
		else if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			weaponChoice = 1;
			animator.SetInteger("Weapon", 1); 
		}

		//Attack animation control
		if (animator.GetBool ("Attack") == true)
		{
			animator.SetBool ("Attack", false);
		}
		
		if(animator.GetBool ("Attack") == false && Input.GetKeyDown(KeyCode.W))
		{
			animator.SetBool ("Attack", true);
			source.PlayOneShot(swing);
		}


		//actual movement change
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}
	void Flip()//flip animation for sprite
	{
		facingRight = !facingRight; //reverse current direction
		Vector3 theScale = transform.localScale;
		theScale.x *= -1; //flip the transform of the sprite
		transform.localScale = theScale;
	}

	void OnGUI()
	{
		// Lives - top left
		GUI.Label (new Rect (10, 10, 70, 20), "Lives: " + PlayerController.lives.ToString());

		// health - just below lives in top left
		GUI.Label (new Rect (10, 40, 70, 20), "Health: " + PlayerController.health.ToString ()); // convert to hearts later

		// score - top right
		GUI.Label (new Rect (Screen.width - 80, 10, 60, 20), "Score: " + PlayerController.score.ToString ());

		// ammo - bottom left
		GUI.Label (new Rect (10, Screen.height - 30, 70, 20), "Ammo: " + PlayerController.ammo.ToString ());
	}
}