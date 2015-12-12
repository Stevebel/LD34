using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {

	private GameObject ghost;
	private SpriteRenderer ghostSprite;
	private SelectedTower selected;
	public Camera cam;
	public Tower towerPrefab;


	private Transform tf;
	private int overlapCount = 0;
	// Use this for initialization
	void Start () {
		tf = this.GetComponent<Transform>();
		selected = this.GetComponent<SelectedTower> ();
		SelectedTower.OnSelect += initGhost;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = cam.ScreenToWorldPoint (Input.mousePosition);
		target.z = 0;
		tf.position = target;
		if (ghost != null) {
			if (overlapCount > 0) {
				ghostSprite.color = new Vector4 (1, 0, 0, 0.5f);
			} else {
				//Style ghost
				ghostSprite.color = new Vector4 (1, 1, 1, 0.5f);

				//Place on LMB
				if (Input.GetMouseButtonUp (0)) {
					PlaceTower ();
				}
			}
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		overlapCount++;
	}
	void OnTriggerExit2D (){
		overlapCount--;
	}

	void PlaceTower(){
		Tower tower = Instantiate (towerPrefab);

		TowerDefinition def = selected.GetSelected ();

		GameObject graphic = Instantiate (def.graphic);
		graphic.transform.parent = tower.transform;
		graphic.transform.localPosition = Vector3.zero;
		tower.transform.position = tf.position;

		initGhost (def);
	}

	void initGhost(TowerDefinition def){
		if (ghost) {
			Destroy (ghost);
		}
		if (def != null) {
			this.ghost = Instantiate (def.graphic);
			ghost.transform.parent = tf;
			ghost.transform.localPosition = Vector3.zero;
			ghost.GetComponent<Collider2D> ().enabled = false;
			ghostSprite = ghost.GetComponent<SpriteRenderer> ();
			Cursor.visible = false;
		} else {
			Cursor.visible = true;
		}
	}
}
