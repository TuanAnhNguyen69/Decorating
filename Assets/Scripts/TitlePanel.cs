using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : MonoBehaviour {
	public GameObject titlePanel;
	// Use this for initialization
	void Start () {
		titlePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleTitlePanel(){
		if(titlePanel.activeSelf)
			titlePanel.SetActive (!titlePanel.activeSelf);
	}
}
