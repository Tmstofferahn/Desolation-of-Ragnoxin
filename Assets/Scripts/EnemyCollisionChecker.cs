using UnityEngine;
using System.Collections;


public class EnemyCollisionChecker : enemyController {
    public GameObject parent;
    enemyController ec;
    private BoxCollider2D attackArea;
    private int offset;

	void Start () {
        ec = parent.GetComponent<enemyController>();
        //make a new box collider that's the size of parent objects (the enemy) current one
        attackArea = GetComponent<BoxCollider2D>();
        Vector2 newSize = new Vector2(parent.GetComponent<BoxCollider2D>().size.x * 3/4, parent.GetComponent<BoxCollider2D>().size.y);
        
        //offset is used to make sure the new collider is facing in front of the object
        offset = 1;
        if (ec.facing()) offset = -1;
        Vector2 newOffset = new Vector2(attackArea.size.x * offset, 0.0f);

        attackArea.size = newSize;
		attackArea.offset = newOffset;
	}

	void Update () {
        if (ec.facing()) offset = -1;
        else offset = 1;

        Vector2 newOffset = new Vector2(attackArea.size.x * offset, 0.0f);
        attackArea.offset = newOffset;
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("thing!");
			// if enemy runs in to a wall, turn around
			if (other.gameObject.tag == "Obstacle") {
				//ec.turnAround();
				parent.gameObject.SendMessage("turnAround");
			}

        // if player enters collider, start attacking
        if (other.gameObject.tag == "Player") {
            Debug.Log("player!");
            parent.gameObject.SendMessage("Attack");
        }
    }

    //if player leaves collider, stop attacking
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("kbye");
            parent.gameObject.SendMessage("ceaseAttack");
        }
    }
}
