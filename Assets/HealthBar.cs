using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public SpriteRenderer target;
	public Vector2 offset = new Vector2(0,-0.3f);
	public Camera cam;
	public RectTransform canvas;

	private Slider slider;
	private RectTransform sliderTransform;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		sliderTransform = GetComponent<RectTransform> ();
	}

	public void setValue(float val){
		slider.value = val;
	}

	// Update is called once per frame
	void Update () {
		if (target) {
			Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, target.bounds.center - new Vector3(0,target.bounds.extents.y) + new Vector3(offset.x, offset.y));
			sliderTransform.anchoredPosition = screenPoint - canvas.sizeDelta / 2f + offset;
		}
	}
}
