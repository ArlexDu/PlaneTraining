using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
	private GestureSourceManager manager;
	// Use this for initialization
	void Start () {
		manager = GestureSourceManager.Instance;
		List<string> btnsName = new List<string> ();
		btnsName.Add ("LessonOne");
		btnsName.Add ("LessonTwo");
		btnsName.Add ("LessonThree");

		foreach (string btnName in btnsName) {
			GameObject btnobj = GameObject.Find (btnName);
			Button btn = btnobj.GetComponent<Button> ();
			btn.onClick.AddListener (delegate {
				this.OnClick (btnobj);
			});
		}
	}
	
	private void OnClick(GameObject sender){
		switch(sender.name){
		case "LessonOne":
				GameObject.Find ("GestureDetector").GetComponent<GestureController> ().LoadLessonOne ();
				iTween.Stop ();
				break;
			default:
				Debug.Log("none");
				break;
		}
	}
}
