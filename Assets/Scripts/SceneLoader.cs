using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
	[SerializeField]
	private int scene;
	// Updates once per frame
	void Start(){
		
		StartCoroutine (LoadNewScene ());
	}

	void Update() {
		
	}
	IEnumerator LoadNewScene() {
		
		yield return new WaitForSeconds(5);
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = Application.LoadLevelAsync(scene);
		while (!async.isDone) {
			yield return null;
		}

	}

}