using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforPanel : MonoBehaviour {

	public GameObject inforPanel;
	// Use this for initialization
	void Start () {
		inforPanel.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public void toggleInforPanel(){
		inforPanel.SetActive (!inforPanel.activeSelf);
	}
}
