using UnityEngine;
using System.Collections;

public class Consumer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		Creep creep = collider.GetComponent<Creep> ();
		if (creep != null) {
			Debug.Log ("I'm eating");
			Debug.Log (collider.gameObject);
			creep.GetEaten ();
		} else {
			Debug.Log ("I don't want to eat");
			Debug.Log (collider.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
