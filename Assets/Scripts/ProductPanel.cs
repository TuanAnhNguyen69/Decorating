using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPanel : MonoBehaviour {

	public GameObject productPanel;
	// Use this for initialization
	void Start () {
		productPanel = GameObject.Find ("ProductPanel");
		productPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleProductPanel(){
		productPanel.SetActive (!productPanel.activeSelf);
	}
}
