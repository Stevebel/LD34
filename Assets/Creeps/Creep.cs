using UnityEngine;
using System.Collections;

enum CreepState{
	MOVING,
	RUNNING,
	STOPPING,
	DECIDING,
	EATEN
}

public class Creep : MonoBehaviour {
	public float acceleration;
	public float topWalkSpeed;
	public float topRunSpeed;
	public float fillFactor = 1f;
	public Vector2 direction;

	private Rigidbody2D rb;

	private int framesInState = 0;
	private CreepState state = CreepState.STOPPING;
	private Consumer consumer;

	private const float STOPPED_THRESHOLD = 0.1f;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Random.seed = (int)(Random.value * int.MaxValue);
	}

	public void GetEaten(Consumer c){
		if (state != CreepState.EATEN) {
			consumer = c;
			ChangeState (CreepState.EATEN);
		}
	}

	void FixedUpdate () {
		//Debug.Log (state.ToString () + " " + framesInState);
		switch (state) {
		case CreepState.STOPPING:
			if (rb.velocity.sqrMagnitude < STOPPED_THRESHOLD) {
				ChangeState (CreepState.DECIDING);
			}
			break;
		case CreepState.RUNNING:
			Move (topRunSpeed);
			if ((rb.velocity.sqrMagnitude < STOPPED_THRESHOLD && framesInState > 5) || framesInState > (Random.value * 200 + 30)) {
				ChangeState (CreepState.DECIDING);
			}
			break;
		case CreepState.MOVING:
			Move (topWalkSpeed);
			if ((rb.velocity.sqrMagnitude < STOPPED_THRESHOLD && framesInState > 10) || framesInState > (Random.value * 500 + 100)) {
				ChangeState (CreepState.STOPPING);
			}
			break;
		case CreepState.DECIDING:
			float angle = Random.value * Mathf.PI * 2;
			direction = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
			ChangeState (CreepState.MOVING);
			break;
		case CreepState.EATEN:
			if (framesInState > 60) {
				consumer.Eat (fillFactor);
				Destroy (gameObject);
			}
			break;
		}
		framesInState++;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (state == CreepState.MOVING) {
			direction = direction * -1;
			float angle = Mathf.Atan2 (direction.y, direction.x);
			float adj = Random.value;
			adj = adj * adj * adj * Mathf.PI * 0.25f;
			angle += Random.value < 0.5 ? -adj : adj;
			direction = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
			ChangeState (CreepState.RUNNING);
		}
	}

	void Move(float topSpeed){
		if (rb.velocity.magnitude < topSpeed) {
			rb.AddForce (acceleration * direction);
		}
	}

	void ChangeState(CreepState newState){
		state = newState;
		framesInState = 0;
	}
}
