using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotPanel : MonoBehaviour {

	public GameObject screenShotPanel;
	// Use this for initialization
	void Start () {
		screenShotPanel = GameObject.Find ("ScreenShotPanel");
		screenShotPanel.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public void toggleScreenShotPanel(){
		screenShotPanel.SetActive (!screenShotPanel.activeSelf);
	}
}
