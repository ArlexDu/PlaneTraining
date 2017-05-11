using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	public GameObject bodymanager;
	private GestureSourceManager manager;
	// Use this for initialization
	void Start () {
		manager = GestureSourceManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log ("Press A");
			manager.RemoveDetectors();
			manager.AddDetector ("recognize");
		}
	}
}
