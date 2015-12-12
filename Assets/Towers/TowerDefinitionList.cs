using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerDefinitionList : MonoBehaviour {
	[SerializeField]
	public TowerDefinition[] definitions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class TowerDefinition 
{
	public string name;
	public float price;
	public GameObject graphic;
}
