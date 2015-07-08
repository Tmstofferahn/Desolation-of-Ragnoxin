using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {
    public static float health = 2;
    public float distance;
    public float speed;
    public float chaseSpeed;
    public float attackPower;
    private float attackDelay = 2.0f;
    private float attackTimer = 0.0f;
    private float seekRadius = 3.0f;

    private Vector3 startPos;
    private Animator animator;
    private bool attacking;
	public GameObject gameObject;// = GameObject.FindGameObjectWithTag("Enemy");
    private GameObject player;
    private enum UseCase { wander, seek, flee }
	private UseCase useCase;

	//variables for sound
	public AudioClip sound; //stores sound effect. Is input in unity GUI
	private AudioSource source;
	private System.Random rand = new System.Random();

    //int direction = 1; Commented out because it isn't being used
    bool facingLeft;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        facingLeft = true;
        startPos = transform.position;
		attacking = false;
        useCase = UseCase.wander;
		source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
	void Update() {
        if (attacking) {
            //Debug.Log(attacking);
            //updateAttack();
            if (attackTimer == 0.0f) {
                animator.SetTrigger("attack");
                Debug.Log("attack");
                // decrease player health
                PlayerController.takeDamage(1);
            }
            Debug.Log(attackTimer);
            if (attackTimer < attackDelay) attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay) attackTimer = 0.0f;
        }
        else {
            if (useCase == UseCase.wander 
                && (Mathf.Abs(transform.position.x - player.transform.position.x) < seekRadius) 
                && Mathf.Abs(transform.position.y - player.transform.position.y) < 1f)  {
                useCase = UseCase.seek;
            }else{
				useCase = UseCase.wander;
			}
			updateMovement();
        }

		// Will randomly play animal's sound effect
		int randomValue = rand.Next(100);
		if (randomValue < 1) {
			if (!source.isPlaying){
				source.Play();
			}
		}
    }

    IEnumerator updateAttack() {
        if (attackTimer == 0.0f) {
            animator.SetTrigger("attack");
            yield return new WaitForSeconds(0.1f);
            Debug.Log("attack");
            // decrease player health
            PlayerController.takeDamage(1);
        }
        if (attackTimer < attackDelay) attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay) attackTimer = 0.0f;
    }
    IEnumerator killEnemy() {
        animator.SetTrigger("dying");
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }


    void updateMovement() {
        switch (useCase) {
            case enemyController.UseCase.wander:
                if (transform.position.x < startPos.x || transform.position.x > (startPos.x + distance)) {
                    turnAround();
                }
                break;
            case enemyController.UseCase.seek:
                RaycastHit2D leftHit = Physics2D.Raycast(transform.position, -Vector2.right);
                RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right);

                if(leftHit.collider != null) {
                    if(leftHit.collider.name == "Player"){
                        faceDirection(true);
					}
                }

                if(rightHit.collider != null) {
                    if(rightHit.collider.name == "Player"){
                    Debug.Log("to the right");
                    faceDirection(false);
					}
                }               
                break;
            case enemyController.UseCase.flee:
                break;
        }
        transform.position = new Vector3(transform.position.x + speed * GetDirection() * Time.deltaTime, transform.position.y, 0.0f);
    }
    public void Attack() {
        attacking = true;
    }
    public void ceaseAttack() {
        Debug.Log("ceasing");
        attacking = false;
        attackTimer = 0.0f;
		useCase = UseCase.wander;
    }
    public bool facing() {
        return facingLeft;
    }
    public void faceDirection(bool isLeft) {
        facingLeft = isLeft;
        animator.SetBool("moveLeft", isLeft);
    }
    public void turnAround() {
        faceDirection(!facingLeft);
    }
    public float GetDirection() {
        return facingLeft ? -1 : 1;
    }

	public IEnumerator takeDamage(int amount)
	{
        animator.SetTrigger("gotHit");
		enemyController.health -= amount;

		GameObject enemyObj = GameObject.FindGameObjectWithTag("Enemy");
		FloatyText.Create("-"+amount, enemyObj.transform.position, Vector3.up, Color.red, 1); 
		if (enemyController.health < 0) 
		{
			enemyController.health = 0;
		}
		if(enemyController.health == 0)
		{
			yield return StartCoroutine("killEnemy");
		}
	}
}

