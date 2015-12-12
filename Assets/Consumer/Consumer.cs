using UnityEngine;
using System.Collections;

public class Consumer : MonoBehaviour {

	public float maxHealth = 100;
	public float hungerRate = 0.001f;
	public float fillRate = 5f;
	public HealthBar healthBar;

	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	void OnTriggerEnter2D(Collider2D collider){
		Creep creep = collider.GetComponent<Creep> ();
		if (creep != null) {
			Debug.Log ("I'm eating");
			Debug.Log (collider.gameObject);
			creep.GetEaten (this);

		} else {
			Debug.Log ("I don't want to eat");
			Debug.Log (collider.gameObject);
		}
	}

	public void Eat(float fillFactor){
		health = Mathf.Min (maxHealth, health + (fillRate * fillFactor));
	}

	// Update is called once per frame
	void Update () {
		health -= hungerRate;
		healthBar.setValue (health / maxHealth);
	}
}
