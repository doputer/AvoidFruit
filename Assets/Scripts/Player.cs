using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float movePower = 2f;
	public float jumpPower = 1f;
	public int maxHealth = 3;

	private GUIStyle GUIStyle = new GUIStyle();

	Rigidbody2D rigid;
	Animator animator;
	GameObject character;
	SpriteRenderer spriteRenderer;

	Vector3 movement;
	bool isJumping = false;
	bool onJumping = false;

	int health = 3;
	bool isDie = false;
	bool isUnBeatTime = false;

	void Start() {
		rigid = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponentInChildren<Animator>();
		character = GameObject.Find("Character");
		spriteRenderer = character.GetComponentInChildren<SpriteRenderer>();

		health = maxHealth;
	}

	void Update() {
		if (health == 0) {
			if (!isDie) {
				Die();
			}

			return;
		}

		if (Input.GetAxisRaw("Horizontal") == 0) {
			animator.SetBool("isMoving", false);
		}
		else if (Input.GetAxisRaw("Horizontal") < 0) {
			animator.SetBool("isMoving", true);
			animator.SetInteger("WalkDirection", -1);
		}
		else if (Input.GetAxisRaw("Horizontal") > 0) {
			animator.SetBool("isMoving", true);
			animator.SetInteger("WalkDirection", 1);
		}

		if (Input.GetButtonDown("Jump") && !onJumping) {
			isJumping = true;
			onJumping = true;
		}
	}

	void FixedUpdate() {
		if (health == 0) {
			return;
		}

		Move();
		Jump();
	}

	void Move() {
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0) {
			moveVelocity = Vector3.left;
		}
		else if (Input.GetAxisRaw("Horizontal") > 0) {
			moveVelocity = Vector3.right;
		}

		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	void Jump() {
		if (!isJumping) {
			return;
		}

		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;
	}

	void Die() {
		isDie = true;
		SceneManager.LoadScene("Play",LoadSceneMode.Single);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ground") {
			onJumping = false;
		}
		else if (other.gameObject.tag == "Fruit" && !isUnBeatTime) {
			Vector2 attackedVelocity = Vector2.zero;

			if (other.gameObject.transform.position.x > transform.position.x) {
				attackedVelocity = new Vector2(-1f,0);
			}
			else {
				attackedVelocity = new Vector2(1f,0);
			}
			rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

			Destroy(other.gameObject);

			if (health > 1) {
				isUnBeatTime = true;
				StartCoroutine("UnBeatTime");
			}

			health--;
		}
	}

	IEnumerator UnBeatTime() {
		int countTime = 0;

		while (countTime < 10) {
			if (countTime % 2 == 0 ) {
				spriteRenderer.color = new Color32(255,255,255,90);
			}
			else {
				spriteRenderer.color = new Color32(255,255,255,180);
			}

			yield return new WaitForSeconds(0.2f);

			countTime++;
		}

		spriteRenderer.color = new Color32(255,255,255,255);

		isUnBeatTime = false;

		yield return null;
	}

	void OnGUI() {
		// Health
		GUIStyle.fontSize = 70;
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
		GUILayout.BeginVertical();
		GUILayout.Space(8);
		GUILayout.BeginHorizontal();
		GUILayout.Space(12);

		string heart = "";
		for (int i = 0; i < health; i++) {
			heart += "♥ ";
		}
		GUILayout.Label(heart);

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
