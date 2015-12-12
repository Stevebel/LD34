using UnityEngine;
using System.Collections;

public class SelectedTower : MonoBehaviour {

	public TowerDefinitionList definitionList;
	public int selectedIndex = 0;

	public delegate void SelectAction(TowerDefinition def);
	public static event SelectAction OnSelect;

	private int oldIndex = -100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(oldIndex != selectedIndex){
			oldIndex = selectedIndex;
			if (OnSelect != null) {
				OnSelect (GetSelected ());
			}
		}
	}

	public TowerDefinition GetSelected(){
		return selectedIndex < 0 ? null : definitionList.definitions [selectedIndex];
	}
}
