using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotButton : MonoBehaviour {
	public Camera camera;
	int resWidth = 2560; 
	int resHeight = 1440;
	public GameObject screenShotPanel;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string ScreenShotName(int width,int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.persistentDataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public void TakeHiResShot() {
		if(!System.IO.Directory.Exists(Application.persistentDataPath+"/screenshots")){
			System.IO.Directory.CreateDirectory(Application.persistentDataPath+"/screenshots");
		}
		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		camera.targetTexture = rt;
		Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		camera.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		camera.targetTexture = null;
		RenderTexture.active = null; // JC: added to avoid errors
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		string filename = ScreenShotName(resWidth, resHeight);
		System.IO.File.WriteAllBytes(filename, bytes);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));
		screenShotPanel.SetActive (true);
	}
}

